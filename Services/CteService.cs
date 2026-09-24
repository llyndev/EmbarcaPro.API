using EmbarcaPro.API.Common.Helpers;
using EmbarcaPro.API.Common.Pagination;
using EmbarcaPro.API.Common.Results;
using EmbarcaPro.API.Data;
using EmbarcaPro.API.Enums;
using EmbarcaPro.API.Dtos.Response;
using EmbarcaPro.API.Models;
using EmbarcaPro.API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using EmbarcaPro.API.Extensions;
using EmbarcaPro.API.Dtos.Request;

namespace EmbarcaPro.API.Services
{
    public class CteService(ApplicationDbContext context, ICurrentUser currentUser) : ICteService
    {

        public async Task<ServiceResult<CteResponse>> CreateCteAsync(CreateCteRequest request)
        {
            var strategy = context.Database.CreateExecutionStrategy();

            return await strategy.ExecuteAsync(async () =>
            {
                context.ChangeTracker.Clear();

                await using var transaction = await context.Database.BeginTransactionAsync();

                await context.Database.ExecuteSqlAsync(
                        $"SELECT company_id FROM companies WHERE company_id = {currentUser.CompanyId} FOR UPDATE");

                var company = await context.Companies.FirstOrDefaultAsync(c => c.Id == currentUser.CompanyId);

                if (company is null)
                    return ServiceResult<CteResponse>.Fail("Empresa emitente não encontrada.", ErrorType.NotFound);

                Freight? freight = null;

                if (request.FreightId is int freightId)
                {
                    freight = await context.Freights.FirstOrDefaultAsync(f => f.Id == freightId);

                    if (freight is null)
                        return ServiceResult<CteResponse>.Fail("Frete não encontrado.", ErrorType.NotFound);

                    if (freight.Status == FreightStatus.Canceled)
                        return ServiceResult<CteResponse>.Fail("Não é possível emitir CT-e para um frete cancelado.", ErrorType.Conflict);

                }

                var partnerIds = request.Partners
                    .Select(p => p.PartnerPublicId)
                    .Distinct()
                    .ToList();

                var partners = await context.Partners
                    .Where(p => partnerIds.Contains(p.PublicId) && p.IsActive)
                    .ToDictionaryAsync(p => p.PublicId);

                var missing = partnerIds.Where(id => !partners.ContainsKey(id)).ToList();
                if (missing.Count > 0)
                    return ServiceResult<CteResponse>.Fail("Parceiro(s) não encontrado(s) ou inativo(s).", ErrorType.NotFound);

                var icmsResult = BuildIcms(request.Icms);
                if (!icmsResult.Success)
                    return ServiceResult<CteResponse>.Fail(icmsResult.Message, icmsResult.ErrorType);

                Cte cte;
                try
                {
                    cte = new Cte(
                        company,
                        request.Type,
                        request.ServiceType,
                        request.TransportMode,
                        request.PredominantCfop,
                        request.OriginIbgeCode,
                        request.DestinationIbgeCode,
                        request.TotalServiceValue,
                        request.AmountReceivable,
                        freight);

                    foreach (var p in request.Partners)
                        cte.AddPartner(partners[p.PartnerPublicId], p.Type);

                    foreach (var component in request.FreightComponents)
                        cte.AddFreightComponent(component.Name, component.Value);

                    var cargo = new Cargo(
                        request.Cargo.CargoValue,
                        request.Cargo.PredominantProduct,
                        request.Cargo.OtherCharacteristics);

                    foreach (var q in request.Cargo.Quantities)
                        cargo.AddQuantity(q.UnitCode, q.MeasureType, q.Quantity);

                    cte.SetCargo(cargo);

                    foreach (var invoice in request.ReferencedInvoices)
                        cte.AddReferencedInvoice(invoice.NfeAccessKey, invoice.InvoiceValue, invoice.OrderNumber);

                    cte.SetIcms(icmsResult.Data!);

                    cte.ValidateFreightComposition();
                }
                catch (Exception ex) when (ex is ArgumentException or InvalidOperationException)
                {
                    return ServiceResult<CteResponse>.Fail(ex.Message, ErrorType.Validation);
                }

                try
                {
                    context.Ctes.Add(cte);
                    
                    await context.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch (DbUpdateException)
                {
                    return ServiceResult<CteResponse>.Fail("Não foi possível gerar a numeração do CT-e. Tente novamente.", ErrorType.Conflict);
                }

                return ServiceResult<CteResponse>.Ok(cte.ToResponse(), $"CT-e {cte.Series}/{cte.Number} criado em rascunho.");
            });
        }

        private static ServiceResult<IcmsTax> BuildIcms(CteIcmsRequest icms)
        {
            try
            {
                IcmsTax? tax = icms.Situation switch
                {
                    IcmsTaxSituation.NormalTaxation =>
                        IcmsTax.NormalTaxation(icms.TaxBase!.Value, icms.Rate!.Value),

                    IcmsTaxSituation.TaxationWithReducedBase =>
                        IcmsTax.WithReducedBase(icms.TaxBase!.Value, icms.BaseReductionPercentage!.Value, icms.Rate!.Value),

                    IcmsTaxSituation.Deferred =>
                        IcmsTax.Deferred(icms.TaxBase!.Value, icms.Rate!.Value, icms.DeferredPercentage!.Value),

                    IcmsTaxSituation.Exempt => IcmsTax.Exempt(),
                    IcmsTaxSituation.NotTaxed => IcmsTax.NotTaxed(),

                    _ => null
                };

                return tax is null ? ServiceResult<IcmsTax>.Fail($"Situação tributária {icms.Situation} ainda não é suportada.", ErrorType.Validation) : ServiceResult<IcmsTax>.Ok(tax, "");
            }
            catch (ArgumentException ex)
            {
                return ServiceResult<IcmsTax>.Fail(ex.Message, ErrorType.Validation);
            }
        }

        public async Task<ServiceResult<PagedList<CteListItemResponse>>> GetAllCtesAsync(int page, int pageSize)
        {
            page = page < 1 ? 1 : page;
            pageSize = pageSize is < 1 or > 100 ? 10 : pageSize;

            var query = context.Ctes.AsNoTracking();

            var totalCount = await query.CountAsync();

            var items = await query
                .OrderByDescending(c => c.CreatedAt)
                .ThenByDescending(c => c.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(c => new
                {
                    c.PublicId,
                    c.Series,
                    c.Number,
                    c.Status,
                    c.TotalServiceValue,
                    c.AccessKey,
                    c.IssueDateTime,
                    ShipperName = c.Partners
                        .Where(p => p.Type == PartnerType.Shipper)
                        .Select(p => p.Partner.LegalNameOrFullName)
                        .FirstOrDefault(),
                    ConsigneeName = c.Partners
                        .Where(p => p.Type == PartnerType.Consignee)
                        .Select(p => p.Partner.LegalNameOrFullName)
                        .FirstOrDefault()
                })
                .ToListAsync();

            var response = items.Select(c => new CteListItemResponse(
                c.PublicId,
                c.Series,
                c.Number,
                c.Status.ToResponse(),
                c.TotalServiceValue,
                c.AccessKey,
                c.IssueDateTime,
                c.ShipperName,
                c.ConsigneeName
            )).ToList();

            var pagedList = new PagedList<CteListItemResponse>(response, totalCount, page, pageSize);

            return ServiceResult<PagedList<CteListItemResponse>>.Ok(pagedList, "CT-es listados com sucesso.");
        }

        public async Task<ServiceResult<CteResponse>> GetCteByPublicIdAsync(Guid id)
        {
            var cte = await context.Ctes
                .AsNoTracking()
                .AsSplitQuery()
                .Include(c => c.Company)
                .Include(c => c.Partners).ThenInclude(p => p.Partner)
                .Include(c => c.FreightComponents)
                .Include(c => c.Cargo).ThenInclude(c => c.Quantities)
                .Include(c => c.Icms)
                .Include(c => c.ReferencedInvoices)
                .Include(c => c.Events)
                .FirstOrDefaultAsync(c => c.PublicId == id);

            if (cte == null)
                return ServiceResult<CteResponse>.Fail("CT-e não encontrado.", ErrorType.NotFound);

            return ServiceResult<CteResponse>.Ok(cte.ToResponse(), $"CT-e {cte.Series}/{cte.Number}");
        }

        public async Task<ServiceResult<CteResponse>> PrepareForTransmissionAsync(Guid id)
        {
            var cte = await context.Ctes
                .AsSplitQuery()
                .Include(c => c.Company)
                .Include(c => c.Partners).ThenInclude(p => p.Partner)
                .Include(c => c.FreightComponents)
                .Include(c => c.Cargo).ThenInclude(c => c!.Quantities)
                .Include(c => c.Icms)
                .Include(c => c.ReferencedInvoices)
                .FirstOrDefaultAsync(c => c.PublicId == id);

            if (cte is null)
                return ServiceResult<CteResponse>.Fail("CT-e não encontrado.", ErrorType.NotFound);

            try
            {
                cte.EnsureReadyForTransmission();
                cte.AssignAccessKey();
            }
            catch (InvalidOperationException ex)
            {
                return ServiceResult<CteResponse>.Fail(ex.Message, ErrorType.Conflict);
            }
            catch (ArgumentException ex)
            {
                return ServiceResult<CteResponse>.Fail(ex.Message, ErrorType.Validation);
            }

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                return ServiceResult<CteResponse>.Fail("Conflito ao gerar a chave de acesso. Tente novamente.",
                    ErrorType.Conflict);
            }

            return ServiceResult<CteResponse>.Ok(cte.ToResponse(), $"Chave de acesso gerada: {cte.AccessKey}");
        }

        public Task<ServiceResult<CteResponse>> AuthorizeCteAsync(Guid id)
            => ChangeStatusAsync(id, cte => cte.Authorize(), "CT-e autorizado com sucesso.");

        public Task<ServiceResult<CteResponse>> CancelCteAsync(Guid id)
            => ChangeStatusAsync(id, cte => cte.Cancel(), "CT-e cancelado com sucesso.");

        public Task<ServiceResult<CteResponse>> DenyCteAsync(Guid id)
            => ChangeStatusAsync(id, cte => cte.Deny(), "CT-e denegado.");

        private async Task<ServiceResult<CteResponse>> ChangeStatusAsync(
            Guid id, Action<Cte> transition, string successMessage)
        {
            var cte = await context.Ctes
                .Include(c => c.FreightComponents)
                .FirstOrDefaultAsync(c => c.PublicId == id);

            if (cte == null)
                return ServiceResult<CteResponse>.Fail("CT-e não encontrado.", ErrorType.NotFound);

            try
            {
                transition(cte);
            }
            catch (InvalidOperationException ex)
            {
                return ServiceResult<CteResponse>.Fail(ex.Message, ErrorType.Conflict);
            }

            await context.SaveChangesAsync();

            return ServiceResult<CteResponse>.Ok(cte.ToResponse(), successMessage);
        }
    }
}

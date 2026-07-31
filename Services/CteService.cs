using EmbarcaPro.API.Common.Helpers;
using EmbarcaPro.API.Common.Pagination;
using EmbarcaPro.API.Common.Results;
using EmbarcaPro.API.Data;
using EmbarcaPro.API.Enums;
using EmbarcaPro.API.Dtos.Request;
using EmbarcaPro.API.Dtos.Response;
using EmbarcaPro.API.Models;
using EmbarcaPro.API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using EmbarcaPro.API.Extensions;

namespace EmbarcaPro.API.Services
{
    public class CteService(ApplicationDbContext context) : ICteService
    {
        //public async Task<ServiceResult<CteResponse>> CreateCteAsync(CreateCteRequest request)
        //{
        //    var freightExists = await context.Freights.AnyAsync(f => f.Id == request.FreightId);
        //    if (!freightExists)
        //        return ServiceResult<CteResponse>.Fail("Viagem (frete) não encontrada.", ErrorType.NotFound);

        //    var numberExists = await context.Ctes.AnyAsync(c => c.Number == request.Number);
        //    if (numberExists)
        //        return ServiceResult<CteResponse>.Fail("Já existe um CT-e com este número.", ErrorType.Conflict);

        //    Cte cte;
        //    try
        //    {
        //        cte = new Cte(request.FreightId, request.Number, request.TotalServiceValue, request.AmountReceivable);

        //        foreach (var component in request.FreightComponents)
        //            cte.AddFreightComponent(component.Name, component.Value);
        //    }
        //    catch (ArgumentException ex)
        //    {
        //        return ServiceResult<CteResponse>.Fail(ex.Message, ErrorType.Validation);
        //    }

        //    await context.Ctes.AddAsync(cte);
        //    await context.SaveChangesAsync();

        //    return ServiceResult<CteResponse>.Ok(cte.ToResponse(), "CT-e criado com sucesso!");
        //}

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
                .Include(c => c.FreightComponents)
                .Include(c => c.Cargo).ThenInclude(c => c.Quantities)
                .FirstOrDefaultAsync(c => c.PublicId == id);

            if (cte == null)
                return ServiceResult<CteResponse>.Fail("CT-e não encontrado.", ErrorType.NotFound);

            return ServiceResult<CteResponse>.Ok(cte.ToResponse(), $"CT-e {id}");
        }

        public async Task<ServiceResult<CteResponse>> AuthorizeCteAsync(Guid id)
        {
            var cte = await context.Ctes
                .Include(c => c.FreightComponents)
                .FirstOrDefaultAsync();

            if (cte == null)
                return ServiceResult<CteResponse>.Fail("CT-e não encontrado", ErrorType.NotFound);

            try
            {
                cte.Authorize();
            } catch (InvalidOperationException ex)
            {
                return ServiceResult<CteResponse>.Fail(ex.Message, ErrorType.Conflict);
            }

            await context.SaveChangesAsync();

            return ServiceResult<CteResponse>.Ok(cte.ToResponse(), "CT-e autorizado com sucesso.");
        }

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
                return ServiceResult<CteResponse>.Fail(ex.Message, ErrorType.Validation);
            }

            await context.SaveChangesAsync();

            return ServiceResult<CteResponse>.Ok(cte.ToResponse(), successMessage);
        }
    }
}

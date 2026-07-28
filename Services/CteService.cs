using EmbarcaPro.API.Common.Helpers;
using EmbarcaPro.API.Common.Pagination;
using EmbarcaPro.API.Common.Results;
using EmbarcaPro.API.Data;
using EmbarcaPro.API.Dtos.Request;
using EmbarcaPro.API.Dtos.Response;
using EmbarcaPro.API.Models;
using EmbarcaPro.API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EmbarcaPro.API.Services
{
    public class CteService(ApplicationDbContext context) : ICteService
    {
        public async Task<ServiceResult<CteResponse>> CreateCteAsync(CreateCteRequest request)
        {
            var freightExists = await context.Freights.AnyAsync(f => f.Id == request.FreightId);
            if (!freightExists)
                return ServiceResult<CteResponse>.Fail("Viagem (frete) não encontrada.", ErrorType.NotFound);

            var numberExists = await context.Ctes.AnyAsync(c => c.Number == request.Number);
            if (numberExists)
                return ServiceResult<CteResponse>.Fail("Já existe um CT-e com este número.", ErrorType.Conflict);

            Cte cte;
            try
            {
                cte = new Cte(request.FreightId, request.Number, request.TotalServiceValue, request.AmountReceivable);

                foreach (var component in request.FreightComponents)
                    cte.AddFreightComponent(component.Name, component.Value);
            }
            catch (ArgumentException ex)
            {
                return ServiceResult<CteResponse>.Fail(ex.Message, ErrorType.Validation);
            }

            await context.Ctes.AddAsync(cte);
            await context.SaveChangesAsync();

            return ServiceResult<CteResponse>.Ok(ToResponse(cte), "CT-e criado com sucesso!");
        }

        public async Task<ServiceResult<PagedList<CteResponse>>> GetAllCtesAsync(int page, int pageSize)
        {
            page = page < 1 ? 1 : page;
            pageSize = pageSize is < 1 or > 100 ? 10 : pageSize;

            var query = context.Ctes.AsNoTracking();

            var totalCount = await query.CountAsync();

            var items = await query
                .Include(c => c.FreightComponents)
                .OrderByDescending(c => c.CreatedAt)
                .ThenByDescending(c => c.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var response = items.Select(ToResponse).ToList();
            var pagedList = new PagedList<CteResponse>(response, totalCount, page, pageSize);

            return ServiceResult<PagedList<CteResponse>>.Ok(pagedList, "CT-es listados com sucesso.");
        }

        public async Task<ServiceResult<CteResponse>> GetCteByIdAsync(int id)
        {
            var cte = await context.Ctes
                .AsNoTracking()
                .Include(c => c.FreightComponents)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (cte == null)
                return ServiceResult<CteResponse>.Fail("CT-e não encontrado.", ErrorType.NotFound);

            return ServiceResult<CteResponse>.Ok(ToResponse(cte), $"CT-e {id}");
        }

        public Task<ServiceResult<CteResponse>> AuthorizeCteAsync(int id)
            => ChangeStatusAsync(id, cte => cte.Authorize(), "CT-e autorizado com sucesso.");

        public Task<ServiceResult<CteResponse>> CancelCteAsync(int id)
            => ChangeStatusAsync(id, cte => cte.Cancel(), "CT-e cancelado com sucesso.");

        public Task<ServiceResult<CteResponse>> DenyCteAsync(int id)
            => ChangeStatusAsync(id, cte => cte.Deny(), "CT-e denegado.");

        private async Task<ServiceResult<CteResponse>> ChangeStatusAsync(
            int id, Action<Cte> transition, string successMessage)
        {
            var cte = await context.Ctes
                .Include(c => c.FreightComponents)
                .FirstOrDefaultAsync(c => c.Id == id);

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

            return ServiceResult<CteResponse>.Ok(ToResponse(cte), successMessage);
        }

        private static CteResponse ToResponse(Cte cte)
        {
            return new CteResponse(
                cte.Id,
                cte.Number,
                cte.FreightId,
                cte.Status.ToString(),
                EmbarcaProEnumsList.GetCteStatusDescription(cte.Status),
                cte.TotalServiceValue,
                cte.AmountReceivable,
                cte.AccessKey,
                cte.CreatedAt,
                cte.AuthorizedAt,
                cte.CanceledAt,
                cte.FreightComponents
                    .Select(fc => new CteFreightComponentResponse(fc.Name, fc.Value))
                    .ToList());
        }
    }
}

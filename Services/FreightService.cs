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
    public class FreightService(ApplicationDbContext context) : IFreightService
    {

        public async Task<ServiceResult<FreightResponse>> CreateFreightAsync(CreateFreightRequest request)
        {
            // Validar != entre origem e destino
            if (request.OriginFacilityId == request.DestinationFacilityId)
            {
                return ServiceResult<FreightResponse>.Fail(
                    "A origem e o destino da viagem não podem ser os mesmos.",
                    ErrorType.Validation
                    );
                    
            }

            var driver = await context.Drivers.FindAsync(request.DriverId);
            var truck = await context.Trucks.FindAsync(request.TruckId);
            var trailer = await context.Trailers.FindAsync(request.TrailerId);

            var originExists = await context.Facilities.AnyAsync(f => f.Id == request.OriginFacilityId && f.IsActive);
            var destExists = await context.Facilities.AnyAsync(f => f.Id == request.DestinationFacilityId && f.IsActive);

            if (driver == null || !driver.IsActive)
                return ServiceResult<FreightResponse>.Fail(
                    "Motorista não encontrado",
                    ErrorType.NotFound);

            if (truck == null)
                return ServiceResult<FreightResponse>.Fail(
                    "Caminhão não encontrado.",
                    ErrorType.NotFound);

            if (!truck.IsAvailable)
                return ServiceResult<FreightResponse>.Fail(
                    $"O caminhão placa {truck.LicensePlate} já está alocado em outra viagem ou manutenção.",
                    ErrorType.Conflict);

            if (trailer == null)
                return ServiceResult<FreightResponse>.Fail(
                    "Carreta não encontrada.",
                    ErrorType.NotFound);

            if (!trailer.IsAvailable)
                return ServiceResult<FreightResponse>.Fail(
                    $"A carreta placa {trailer.LicensePlate} já está em uso.",
                    ErrorType.Conflict);

            if (!originExists || !destExists)
                return ServiceResult<FreightResponse>.Fail(
                    "Usina de origem ou destino não encontrada ou inativa.",
                    ErrorType.NotFound);

            var newFreight = new Freight(
                driverId: request.DriverId,
                truckId: request.TruckId,
                trailerId: request.TrailerId,
                originFacilityId: request.OriginFacilityId,
                destinationFacilityId: request.DestinationFacilityId,
                cargoDescription: request.CargoDescription,
                estimatedWeightKg: request.EstimatedWeightKg,
                freightValue: request.FreightValue
            );

            truck.MarkAsUnavailable();
            trailer.MarkAsUnavailable();

            await context.Freights.AddAsync( newFreight );
            await context.SaveChangesAsync();

            var response = new FreightResponse(
                newFreight.Id,
                driver.Name,
                truck.LicensePlate,
                trailer.LicensePlate,
                newFreight.OriginFacility.Address.City,
                newFreight.DestinationFacility.Address.City,
                newFreight.CargoDescription,
                newFreight.Status.ToString(),
                newFreight.FreightValue,
                newFreight.CreatedAt);

            return ServiceResult<FreightResponse>.Ok(response, "Viagem criada com sucesso!");
        }

        public async Task<ServiceResult<PagedList<FreightResponse>>> GetAllFreightsAsync(int page, int pageSize)
        {
            var query = context.Freights.AsNoTracking();

            var totalCount = await query.CountAsync();

            var items = await query
                .OrderByDescending(f => f.CreatedAt)
                .ThenByDescending(f => f.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(f => new FreightResponse(
                    f.Id,
                    f.Driver.Name,
                    f.Truck.LicensePlate,
                    f.Trailer.LicensePlate,
                    f.OriginFacility.Address.City,
                    f.DestinationFacility.Address.City,
                    f.CargoDescription,
                    f.Status.ToString(),
                    f.FreightValue,
                    f.CreatedAt
                ))
                .ToListAsync();

            var pagedList = new PagedList<FreightResponse>(items, totalCount, page, pageSize);

            return ServiceResult<PagedList<FreightResponse>>.Ok(pagedList, "Fretes listados com sucesso.");
        }

    }
}

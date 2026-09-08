using EmbarcaPro.API.Common.Pagination;
using EmbarcaPro.API.Common.Results;
using EmbarcaPro.API.Data;
using EmbarcaPro.API.Dtos.Request;
using EmbarcaPro.API.Dtos.Response;
using EmbarcaPro.API.Enums;
using EmbarcaPro.API.Models;
using EmbarcaPro.API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EmbarcaPro.API.Services
{
    public class FreightService(ApplicationDbContext context, ICurrentUser currentUser) : IFreightService
    {

        public async Task<ServiceResult<FreightResponse>> CreateFreightAsync(CreateFreightRequest request)
        {
            // Validar != entre origem e destino
            if (request.OriginId == request.DestinationId)
            {
                return ServiceResult<FreightResponse>.Fail(
                    "A origem e o destino da viagem não podem ser os mesmos.",
                    ErrorType.Validation
                    );
                    
            }

            var company = await context.Companies
                .FirstOrDefaultAsync(c => c.Id == currentUser.CompanyId);

            if (company == null)
                return ServiceResult<FreightResponse>.Fail("Empresa não encontrada.", ErrorType.NotFound);

            var driver = await context.Drivers.FindAsync(request.DriverId);
            var truck = await context.Trucks.FindAsync(request.TruckId);
            var trailer = await context.Trailers.FindAsync(request.TrailerId);

            var originExists = await context.Partners.AnyAsync(f => f.Id == request.OriginId && f.IsActive);
            var destExists = await context.Partners.AnyAsync(f => f.Id == request.DestinationId && f.IsActive);

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
                company,
                driverId: request.DriverId,
                truckId: request.TruckId,
                trailerId: request.TrailerId,
                originId: request.OriginId,
                destinationId: request.DestinationId,
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
                newFreight.Origin.Address.City,
                newFreight.Destination.Address.City,
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
                    f.Origin.Address.City,
                    f.Destination.Address.City,
                    f.CargoDescription,
                    f.Status.ToString(),
                    f.FreightValue,
                    f.CreatedAt
                ))
                .ToListAsync();

            var pagedList = new PagedList<FreightResponse>(items, totalCount, page, pageSize);

            return ServiceResult<PagedList<FreightResponse>>.Ok(pagedList, "Fretes listados com sucesso.");
        }

        public async Task<ServiceResult<FreightResponse>> GetFreightById(int id)
        {
            var response = await context.Freights
                .AsNoTracking()
                .Where(f => f.Id == id)
                .Select(f => new FreightResponse(
                    f.Id,
                    f.Driver.Name,
                    f.Truck.LicensePlate,
                    f.Trailer.LicensePlate,
                    f.Origin.Address.City,
                    f.Destination.Address.City,
                    f.CargoDescription,
                    f.Status.ToString(),
                    f.FreightValue,
                    f.CreatedAt
                ))
                .FirstOrDefaultAsync();

            if (response == null)
            {
                return ServiceResult<FreightResponse>.Fail("Viagem não encontrada", ErrorType.NotFound);
            }

            return ServiceResult<FreightResponse>.Ok(response, $"Viagem {id}");
        }

        public async Task<ServiceResult<FreightResponse>> StartTripAsync(int id)
        {
            var freight = await context.Freights
                .Include(f => f.Driver)
                .Include(f => f.Truck)
                .Include(f => f.Trailer)
                .Include(f => f.Origin)
                .Include(f => f.Destination)
                .FirstOrDefaultAsync(f => f.Id == id);

            if (freight == null)
            {
                return ServiceResult<FreightResponse>.Fail("Viagem não encontrada.", ErrorType.NotFound);
            }

            try
            {
                freight.StartTrip();
            }
            catch(InvalidOperationException ex)
            {
                return ServiceResult<FreightResponse>.Fail(ex.Message, ErrorType.Validation);
            }

            await context.SaveChangesAsync();

            // TODO: CRIAR ToResponse para não repetir.
            var response = new FreightResponse(
                freight.Id,
                freight.Driver.Name,
                freight.Truck.LicensePlate,
                freight.Trailer.LicensePlate,
                freight.Origin.Address.City,
                freight.Destination.Address.City,
                freight.CargoDescription,
                freight.Status.ToString(),
                freight.FreightValue,
                freight.CreatedAt
            );

            return ServiceResult<FreightResponse>.Ok(response, $"Viagem{id}");
        }

        public async Task<ServiceResult<FreightResponse>> FinishTripAsync(int id)
        {
            var freight = await context.Freights
                .Include(f => f.Driver)
                .Include(f => f.Truck)
                .Include(f => f.Trailer)
                .Include(f => f.Origin)
                .Include(f => f.Destination)
                .FirstOrDefaultAsync(f => f.Id == id);

            if (freight == null)
            {
                return ServiceResult<FreightResponse>.Fail("Viagem não encontrada.", ErrorType.NotFound);
            }

            try
            {
                freight.FinishTrip();
            }
            catch (InvalidOperationException ex)
            {
                return ServiceResult<FreightResponse>.Fail(ex.Message, ErrorType.Validation);
            }

            await context.SaveChangesAsync();

            var response = new FreightResponse(
                freight.Id,
                freight.Driver.Name,
                freight.Truck.LicensePlate,
                freight.Trailer.LicensePlate,
                freight.Origin.Address.City,
                freight.Destination.Address.City,
                freight.CargoDescription,
                freight.Status.ToString(),
                freight.FreightValue,
                freight.CreatedAt
            );

            return ServiceResult<FreightResponse>.Ok(response, $"Viagem{id}");
        }

        public async Task<ServiceResult<FreightResponse>> CancelTripAsyncs(int id)
        {
            var freight = await context.Freights
                .Include(f => f.Driver)
                .Include(f => f.Truck)
                .Include(f => f.Trailer)
                .Include(f => f.Origin)
                .Include(f => f.Destination)
                .FirstOrDefaultAsync(f => f.Id == id);

            if (freight == null)
            {
                return ServiceResult<FreightResponse>.Fail("Viagem não encontrada.", ErrorType.NotFound);
            }

            try
            {
                freight.CancelTrip();
            }
            catch (InvalidOperationException ex)
            {
                return ServiceResult<FreightResponse>.Fail(ex.Message, ErrorType.Validation);
            }

            await context.SaveChangesAsync();

            var response = new FreightResponse(
                freight.Id,
                freight.Driver.Name,
                freight.Truck.LicensePlate,
                freight.Trailer.LicensePlate,
                freight.Origin.Address.City,
                freight.Destination.Address.City,
                freight.CargoDescription,
                freight.Status.ToString(),
                freight.FreightValue,
                freight.CreatedAt
            );

            return ServiceResult<FreightResponse>.Ok(response, $"Viagem{id}");
        }

    }
}

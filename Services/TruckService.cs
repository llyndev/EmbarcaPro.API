using EmbarcaPro.API.Common.Results;
using EmbarcaPro.API.Data;
using EmbarcaPro.API.Dtos.Request;
using EmbarcaPro.API.Models;
using EmbarcaPro.API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EmbarcaPro.API.Services
{
    public class TruckService(ApplicationDbContext context) : ITruckService
    {

        public async Task<IEnumerable<Truck>> GetAllTrucksAsync()
        {
            return await context.Trucks.ToListAsync();
        }

        public async Task<ServiceResult<Truck>> AddTruckAsync(CreateTruckRequest request)
        {
            // Verificar se a placa já existe.
            var plateExists = await context.Trucks.AnyAsync(t => t.LicensePlate == request.LicensePlate);

            if (plateExists)
            {
                return ServiceResult<Truck>.Fail("Já existe um caminhão com esta placa.", ErrorType.Conflict);
            }

            var truck = new Truck(
                licensePlate: request.LicensePlate,
                truckAxle: request.TruckAxle,
                brand: request.Brand,
                model: request.Model,
                maxCapacityKg: request.MaxCapacityKg
            );

            await context.Trucks.AddAsync(truck);
            await context.SaveChangesAsync();

            return ServiceResult<Truck>.Ok(truck, "Caminhão cadastrado na frota com sucesso!");

        }

        public async Task<ServiceResult<Truck>> GetTruckByPlateAsync(string plate)
        {

            var licensePlate = await context.Trucks.FirstOrDefaultAsync(t => t.LicensePlate == plate);

            if (licensePlate == null)
            {
                return ServiceResult<Truck>.Fail("Caminhão não encontrado com esta placa.", ErrorType.NotFound);
            }

            return ServiceResult<Truck>.Ok(licensePlate, "Caminhão encontrado");
        }

    }
}

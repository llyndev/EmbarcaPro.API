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

        public async Task<(bool Success, string Message, Truck? Data)> AddTruckAsync(CreateTruckRequest request)
        {
            // Verificar se a placa já existe.
            var plateExists = await context.Trucks.AnyAsync(t => t.LicensePlate == request.LicensePlate);

            if (plateExists)
            {
                return (false, "Já existe um caminhão com esta placa.", null);
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

            return (true, "Caminhão cadastrado na frota com sucesso!", truck);


        
        }

    }
}

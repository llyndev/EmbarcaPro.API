using EmbarcaPro.API.Data;
using EmbarcaPro.API.Dtos.Request;
using EmbarcaPro.API.Models;
using EmbarcaPro.API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EmbarcaPro.API.Services
{
    public class TrailerService(ApplicationDbContext context) : ITrailerService
    {
        public async Task<(bool Success, string Message, Trailer? Trailer)> AddTrailerAsync(CreateTrailerRequest request)
        {
            var cleanPlate = request.LicensePlate.Replace("-", "").Replace(" ", "").ToUpper().Trim();

            var plateExists = await context.Trailers.AnyAsync(t => t.LicensePlate == cleanPlate);

            if (plateExists)
            {
                return (false, $"A carreta com a placa {cleanPlate} já está cadastrada no sistema.", null);
            }

            var newTrailer = new Trailer(
                licensePlate: request.LicensePlate,
                trailerAxle: request.TrailerAxle,
                type: request.Type,
                brand: request.Brand,
                maxCapacityKg: request.MaxCapacityKg,
                cubicMetersVolume: request.CubicMetersVolume
                
            );

            await context.Trailers.AddAsync(newTrailer);
            await context.SaveChangesAsync();

            return (true, "Carreta cadastrada com sucesso", newTrailer);
        }
    }
}

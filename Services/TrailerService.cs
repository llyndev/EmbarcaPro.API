using EmbarcaPro.API.Common.Results;
using EmbarcaPro.API.Data;
using EmbarcaPro.API.Dtos.Request;
using EmbarcaPro.API.Models;
using EmbarcaPro.API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EmbarcaPro.API.Services
{
    public class TrailerService(ApplicationDbContext context) : ITrailerService
    {
        public async Task<ServiceResult<Trailer>> AddTrailerAsync(CreateTrailerRequest request)
        {
            var cleanPlate = request.LicensePlate.Replace("-", "").Replace(" ", "").ToUpper().Trim();

            var plateExists = await context.Trailers.AnyAsync(t => t.LicensePlate == cleanPlate);

            if (plateExists)
            {
                return ServiceResult<Trailer>.Fail($"A carreta com a placa {cleanPlate} já está cadastrada no sistema.", ErrorType.Conflict);
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

            return ServiceResult<Trailer>.Ok(newTrailer, "Carreta cadastrada com sucesso");
        }

        public async Task<ServiceResult<Trailer>> GetTrailerByPlateAsync(string plate)
        {
            var licensePlate = await context.Trailers.FirstOrDefaultAsync(t => t.LicensePlate == plate);

            if (licensePlate == null)
            {

                return ServiceResult<Trailer>.Fail("Carreta não encontrada com esta placa.", ErrorType.NotFound);
            }

            return ServiceResult<Trailer>.Ok(licensePlate, "Carreta encontrada.");
        }

        public async Task<IEnumerable<Trailer>> GetAllTrailersAsync()
        {
            return await context.Trailers.ToListAsync();
        }
    }
}

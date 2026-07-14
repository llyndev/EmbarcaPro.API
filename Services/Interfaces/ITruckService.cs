using EmbarcaPro.API.Common.Results;
using EmbarcaPro.API.Dtos.Request;
using EmbarcaPro.API.Models;

namespace EmbarcaPro.API.Services.Interfaces
{
    public interface ITruckService
    {
        // Método de listagem
        // TODO: RETONAR 'TruckResponseDto'
        Task<IEnumerable<Truck>> GetAllTrucksAsync();

        Task<ServiceResult<Truck>> AddTruckAsync(CreateTruckRequest request);
    }
}

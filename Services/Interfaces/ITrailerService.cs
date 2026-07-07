using EmbarcaPro.API.Dtos.Request;
using EmbarcaPro.API.Models;

namespace EmbarcaPro.API.Services.Interfaces
{
    public interface ITrailerService
    {
        Task<(bool Success, string Message, Trailer? Trailer)> AddTrailerAsync(CreateTrailerRequest request);
    }
}

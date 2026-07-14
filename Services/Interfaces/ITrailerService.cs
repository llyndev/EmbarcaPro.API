using EmbarcaPro.API.Common.Results;
using EmbarcaPro.API.Dtos.Request;
using EmbarcaPro.API.Models;

namespace EmbarcaPro.API.Services.Interfaces
{
    public interface ITrailerService
    {
        Task<ServiceResult<Trailer>> AddTrailerAsync(CreateTrailerRequest request);
    }
}

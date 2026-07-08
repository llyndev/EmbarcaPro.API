using EmbarcaPro.API.Dtos.Request;
using EmbarcaPro.API.Models;

namespace EmbarcaPro.API.Services.Interfaces
{
    public interface IFacilityService
    {

        Task<(bool Success, string Message, Facility? Facility)> AddFacilityAsync(CreateFacilityRequest request);

    }
}

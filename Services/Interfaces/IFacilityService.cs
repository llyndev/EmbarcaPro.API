using EmbarcaPro.API.Common.Results;
using EmbarcaPro.API.Dtos.Request;
using EmbarcaPro.API.Models;

namespace EmbarcaPro.API.Services.Interfaces
{
    public interface IFacilityService
    {

        Task<ServiceResult<Facility>> AddFacilityAsync(CreateFacilityRequest request);

    }
}

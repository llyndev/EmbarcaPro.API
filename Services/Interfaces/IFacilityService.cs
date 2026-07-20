using EmbarcaPro.API.Common.Results;
using EmbarcaPro.API.Dtos.Request;
using EmbarcaPro.API.Models;

namespace EmbarcaPro.API.Services.Interfaces
{
    public interface IFacilityService
    {

        Task<ServiceResult<Facility>> AddFacilityAsync(CreateFacilityRequest request);
        Task<List<Facility>> GetAllFacilitiesAsync();
        Task<ServiceResult<Facility>> GetFacilityByCnpjAsnyc(string cnpj);

        Task<ServiceResult<List<Facility>>> GetFacilitiesByNameAsync(string name);

    }
}

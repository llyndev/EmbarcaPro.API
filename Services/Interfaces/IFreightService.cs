using EmbarcaPro.API.Common.Pagination;
using EmbarcaPro.API.Common.Results;
using EmbarcaPro.API.Dtos.Request;
using EmbarcaPro.API.Dtos.Response;
using EmbarcaPro.API.Models;

namespace EmbarcaPro.API.Services.Interfaces
{
    public interface IFreightService
    {
        Task<ServiceResult<FreightResponse>> CreateFreightAsync(CreateFreightRequest request);
        Task<ServiceResult<PagedList<FreightResponse>>> GetAllFreightsAsync(int page, int pageSize);
        Task<ServiceResult<FreightResponse>> GetFreightById(int id);
    }
}

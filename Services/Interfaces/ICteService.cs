using EmbarcaPro.API.Common.Pagination;
using EmbarcaPro.API.Common.Results;
using EmbarcaPro.API.Dtos.Request;
using EmbarcaPro.API.Dtos.Response;

namespace EmbarcaPro.API.Services.Interfaces
{
    public interface ICteService
    {
        Task<ServiceResult<CteResponse>> CreateCteAsync(CreateCteRequest request);

        Task<ServiceResult<PagedList<CteResponse>>> GetAllCtesAsync(int page, int pageSize);

        Task<ServiceResult<CteResponse>> GetCteByIdAsync(int id);

        Task<ServiceResult<CteResponse>> AuthorizeCteAsync(int id);

        Task<ServiceResult<CteResponse>> CancelCteAsync(int id);

        Task<ServiceResult<CteResponse>> DenyCteAsync(int id);
    }
}

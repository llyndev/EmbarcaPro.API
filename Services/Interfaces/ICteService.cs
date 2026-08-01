using EmbarcaPro.API.Common.Pagination;
using EmbarcaPro.API.Common.Results;
using EmbarcaPro.API.Dtos.Response;

namespace EmbarcaPro.API.Services.Interfaces
{
    public interface ICteService
    {
        
        Task<ServiceResult<PagedList<CteListItemResponse>>> GetAllCtesAsync(int page, int pageSize);

        Task<ServiceResult<CteResponse>> GetCteByPublicIdAsync(Guid id);

        Task<ServiceResult<CteResponse>> AuthorizeCteAsync(Guid id);

        Task<ServiceResult<CteResponse>> CancelCteAsync(Guid id);

        Task<ServiceResult<CteResponse>> DenyCteAsync(Guid id);
    }
}

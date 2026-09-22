using EmbarcaPro.API.Common.Pagination;
using EmbarcaPro.API.Common.Results;
using EmbarcaPro.API.Dtos.Request;
using EmbarcaPro.API.Dtos.Response;

namespace EmbarcaPro.API.Services.Interfaces
{
    public interface IPartnerService
    {
        Task<ServiceResult<PartnerResponse>> CreatePartnerAsync(CreatePartnerRequest request);

        Task<ServiceResult<PagedList<PartnerResponse>>> GetAllPartnersAsync(int page, int pageSize, string? search);

        Task<ServiceResult<PartnerResponse>> GetPartnerByPublicIdAsync(Guid id);

        Task<ServiceResult<PartnerResponse>> SetPartnerActiveAsync(Guid id, bool active);

    }
}

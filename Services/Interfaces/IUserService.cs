using EmbarcaPro.API.Common.Results;
using EmbarcaPro.API.Dtos.Request;
using EmbarcaPro.API.Dtos.Response;

namespace EmbarcaPro.API.Services.Interfaces
{
    public interface IUserService
    {

        Task<ServiceResult<UserResponse>> RegisterUserAsync(RegisterRequest request);
        Task<ServiceResult<LoginResponse>> LoginAsync(LoginRequest request);

        Task<List<UserResponse>> GetAllUserResponseAsync();

        Task<ServiceResult<UserResponse>> GetUserByIdResponseAsync(int id);

        Task<ServiceResult<UserResponse>> UpdateUserRoleAsync(UpdateRoleRequest request);

    }
}

using EmbarcaPro.API.Common.Results;
using EmbarcaPro.API.Dtos.Request;
using EmbarcaPro.API.Dtos.Response;

namespace EmbarcaPro.API.Services.Interfaces
{
    public interface IUserService
    {

        Task<(bool Success, string Message)> RegisterUserAsync(RegisterRequest request);
        Task<(bool Success, string Message, LoginResponse? Data)> LoginAsync(LoginRequest request);

        Task<List<UserResponse>> GetAllUserResponseAsync();

        Task<ServiceResult<UserResponse>> GetUserByIdResponseAsync(int id);

        Task<ServiceResult<UserResponse>> UpdateUserRoleAsync(UpdateRoleRequest request);

    }
}

using EmbarcaPro.API.Dtos.Request;

namespace EmbarcaPro.API.Services.Interfaces
{
    public interface IUserService
    {

        Task<(bool Sucess, string Message)> RegisterUserAsync(RegisterRequest request);

    }
}

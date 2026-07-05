using EmbarcaPro.API.Dtos.Request;
using EmbarcaPro.API.Models;

namespace EmbarcaPro.API.Services.Interfaces
{
    public interface IDriverService
    {
        Task<(bool Success, string Message, Driver? Data)> AddDriverAsync(CreateDriverRequest request);
    }
}

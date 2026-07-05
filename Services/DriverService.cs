using EmbarcaPro.API.Data;
using EmbarcaPro.API.Dtos.Request;
using EmbarcaPro.API.Models;
using EmbarcaPro.API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EmbarcaPro.API.Services
{
    public class DriverService(ApplicationDbContext context) : IDriverService
    {

        public async Task<(bool Success, string Message, Driver? Data)> AddDriverAsync(CreateDriverRequest request)
        {
            var cleanCpf = request.Cpf.Replace(".", "").Replace("-", "");

            var existingDriver = await context.Set<Driver>()
                .FirstOrDefaultAsync(d => d.Cpf == cleanCpf || d.Cnh == request.Cnh || d.Email == request.Email);

            if (existingDriver != null)
            {
                return (false, "Credenciais invalidas", null); // TODO: Adicionar detalhes no return
            }

            var driver = new Driver(
                request.Name,
                request.Phone,
                request.Email,
                request.Cpf,
                request.Cnh,
                request.Address);

            await context.Set<Driver>().AddAsync(driver);
            await context.SaveChangesAsync();

            return (true, "Motorista cadastrado com sucesso!", driver);
        }
    }
}

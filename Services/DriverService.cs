using EmbarcaPro.API.Common.Results;
using EmbarcaPro.API.Data;
using EmbarcaPro.API.Dtos.Request;
using EmbarcaPro.API.Models;
using EmbarcaPro.API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EmbarcaPro.API.Services
{
    public class DriverService(ApplicationDbContext context) : IDriverService
    {

        public async Task<ServiceResult<Driver>> AddDriverAsync(CreateDriverRequest request)
        {
            var cleanCpf = request.Cpf.Replace(".", "").Replace("-", "");

            var existingDriver = await context.Set<Driver>()
                .FirstOrDefaultAsync(d => d.Cpf == cleanCpf || d.Cnh == request.Cnh || d.Email == request.Email);

            if (existingDriver != null)
            {
                return ServiceResult<Driver>.Fail("Motorista já existe.", ErrorType.Conflict);
            }

            var address = new Address(
                    street: request.Address.Street,
                    number: request.Address.Number,
                    complement: request.Address.Complement ?? string.Empty,
                    neighborhood: request.Address.Neighborhood,
                    city: request.Address.City,
                    state: request.Address.State,
                    zipCode: request.Address.ZipCode);

            var driver = new Driver(
                request.Name,
                request.Phone,
                request.Email,
                request.Cpf,
                request.Cnh,
                address);

            await context.Set<Driver>().AddAsync(driver);
            await context.SaveChangesAsync();

            return ServiceResult<Driver>.Ok(driver, "Motorista cadastrado com sucesso!");
        }

        public async Task<List<Driver>> GetAllDriversAsync()
        {
            var drivers = await context.Drivers
                .AsNoTracking()
                .ToListAsync();

            return drivers;
        }

        public async Task<ServiceResult<List<Driver>>> GetDriverByName(string name)
        {

            if (string.IsNullOrWhiteSpace(name))
            {
                return ServiceResult<List<Driver>>.Fail("Motorista não encontrado.", ErrorType.Validation);
            }

            var drivers = await context.Drivers
                .AsNoTracking()
                .Where(d => d.Name.Contains(name))
                .ToListAsync();

            if (!drivers.Any()) 
            {
                return ServiceResult<List<Driver>>.Fail("Nenhum motorista encontrado com este nome.", ErrorType.NotFound);
            }

            return ServiceResult<List<Driver>>.Ok(drivers, "Motorista");
        }
    }
}

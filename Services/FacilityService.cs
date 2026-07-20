using EmbarcaPro.API.Common.Results;
using EmbarcaPro.API.Data;
using EmbarcaPro.API.Dtos.Request;
using EmbarcaPro.API.Models;
using EmbarcaPro.API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EmbarcaPro.API.Services
{
    public class FacilityService(ApplicationDbContext context) : IFacilityService
    {
        public async Task<ServiceResult<Facility>> AddFacilityAsync(CreateFacilityRequest request)
        {

            var address = new Address(
                street: request.Address.Street,
                number: request.Address.Number,
                complement: request.Address.Complement ?? string.Empty,
                neighborhood: request.Address.Neighborhood,
                city: request.Address.City,
                state: request.Address.State,
                zipCode: request.Address.ZipCode);

            var newFacility = new Facility(
                name: request.Name,
                address: address,
                cnpj: request.Cnpj
                );

            await context.Facilities.AddAsync(newFacility);
            await context.SaveChangesAsync();

            return ServiceResult<Facility>.Ok(newFacility, "Usina cadastrada com sucesso!");
        }

        public async Task<ServiceResult<Facility>> GetFacilityByCnpjAsnyc(string cnpj)
        {

            if (string.IsNullOrWhiteSpace(cnpj))
            {
                return ServiceResult<Facility>.Fail("CPNJ não pode estar vazio.", ErrorType.Validation);
            }

            string cnpjFormat = cnpj.Replace(".", "").Replace("/", "").Replace("-", "").Trim();

            var facility = await context.Facilities
                .AsNoTracking()
                .FirstOrDefaultAsync(f => f.Cnpj == cnpjFormat);

            if (facility == null)
            {
                return ServiceResult<Facility>.Fail("CPNJ não encontrado", ErrorType.NotFound);
            }

            return ServiceResult<Facility>.Ok(facility, "CNPJ encontrado.");
        }

        public async Task<List<Facility>> GetAllFacilitiesAsync()
        {
            var facilities = await context.Facilities
                .AsNoTracking()
                .ToListAsync();

            return facilities;
        }

        public async Task<ServiceResult<List<Facility>>> GetFacilitiesByNameAsync(string name)
        {

            if (string.IsNullOrWhiteSpace(name))
            {
                return ServiceResult<List<Facility>>.Fail("O nome não pode estar vazio.", ErrorType.Validation);
            }

            var facilities = await context.Facilities
                .AsNoTracking()
                .Where(f => f.Name.Contains(name))
                .ToListAsync();

            if (!facilities.Any())
            {
                return ServiceResult<List<Facility>>.Fail("Fabrica ou Usina não encontrada com este nome.", ErrorType.NotFound);
            }

            return ServiceResult<List<Facility>>.Ok(facilities, "Fabricas e Usinas encontradas.");

        }

    }
}

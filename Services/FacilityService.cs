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

        public async Task<List<Facility>> GetAllFacilitiesAsync()
        {
            var facilities = await context.Facilities
                .AsNoTracking()
                .ToListAsync();

            return facilities;
        }

    }
}

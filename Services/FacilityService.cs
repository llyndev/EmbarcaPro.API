using EmbarcaPro.API.Data;
using EmbarcaPro.API.Dtos.Request;
using EmbarcaPro.API.Models;
using EmbarcaPro.API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EmbarcaPro.API.Services
{
    public class FacilityService(ApplicationDbContext context) : IFacilityService
    {
        public async Task<(bool Success, string Message, Facility? Facility)> AddFacilityAsync(CreateFacilityRequest request)
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

            return (true, "Usina cadastrada com sucesso!", newFacility);
        }

    }
}

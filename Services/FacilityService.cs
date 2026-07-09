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

            var newFacility = new Facility(
                name: request.Name,
                address: request.Address,
                cnpj: request.Cnpj
                );

            await context.Facilities.AddAsync(newFacility);
            await context.SaveChangesAsync();

            return (true, "Usina cadastrada com sucesso!", newFacility);
        }

    }
}

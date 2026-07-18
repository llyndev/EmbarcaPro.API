using EmbarcaPro.API.Dtos.Request;
using EmbarcaPro.API.Enums;
using EmbarcaPro.API.Extensions;
using EmbarcaPro.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmbarcaPro.API.Controller
{
    [ApiController]
    [Route("api/trucks")]
    [Authorize]
    public class TruckController(ITruckService truckService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllTrucks()
        {
            var result = await truckService.GetAllTrucksAsync();

            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = nameof(UserRole.Admin) + "," + nameof(UserRole.Suporte))]
        public async Task<IActionResult> AddTruck([FromBody] CreateTruckRequest request)
        {
            var result = await truckService.AddTruckAsync(request);

            return result.ToActionResult(this, StatusCodes.Status201Created);
        }


    }
}

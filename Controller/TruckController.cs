using EmbarcaPro.API.Dtos.Request;
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
            var trucks = await truckService.GetAllTrucksAsync();
            return Ok(trucks);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddTruck([FromBody] CreateTruckRequest request)
        {
            var (success, message, truck) = await truckService.AddTruckAsync(request);

            if (!success)
            {
                return BadRequest(new { error = message });
            }

            return StatusCode(StatusCodes.Status201Created, new { message, truck });
        }


    }
}

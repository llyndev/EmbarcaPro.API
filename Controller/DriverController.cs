using EmbarcaPro.API.Dtos.Request;
using EmbarcaPro.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmbarcaPro.API.Controller
{
    [ApiController]
    [Route("api/drivers")]
    [Authorize]
    public class DriverController(IDriverService driverService) : ControllerBase
    {

        [HttpPost]
        [Authorize(Roles = "Admin")] // TODO: Resolver para suporte adicionar
        public async Task<IActionResult> AddDriver([FromBody] CreateDriverRequest request)
        {
            var (success, message, driver) = await driverService.AddDriverAsync(request);

            if (!success)
            {
                return BadRequest(new { error = message });
            }

            return StatusCode(StatusCodes.Status201Created, new { message, driver });
        }

    }
}

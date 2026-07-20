using EmbarcaPro.API.Dtos.Request;
using EmbarcaPro.API.Enums;
using EmbarcaPro.API.Extensions;
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
        [Authorize(Roles = nameof(UserRole.Admin) + "," + nameof(UserRole.Suporte))]
        public async Task<IActionResult> AddDriver([FromBody] CreateDriverRequest request)
        {
            var result = await driverService.AddDriverAsync(request);

            return result.ToActionResult(this, StatusCodes.Status201Created);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDriversAsync()
        {
            var drivers = await driverService.GetAllDriversAsync();

            return Ok(drivers);
        }

        [HttpGet("name")]
        public async Task<IActionResult> GetDriversAsync([FromQuery] string? name)
        {

            if (string.IsNullOrWhiteSpace(name))
            {
                var drivers = await driverService.GetAllDriversAsync();
                return Ok(drivers);
            }

            var driver = await driverService.GetDriverByName(name);

            return driver.ToActionResult(this);
        }

    }
}

using EmbarcaPro.API.Dtos.Request;
using EmbarcaPro.API.Enums;
using EmbarcaPro.API.Models;
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

            if (!result.Success)
            {
                return BadRequest(new { error = result.Message });
            }

            return StatusCode(StatusCodes.Status201Created, new { message = result.Message, driver = result.Data });
        }

        [HttpGet]
        public async Task<ActionResult<List<Driver>>> GetAllDriversAsync()
        {
            var response = await driverService.GetAllDriversAsync();

            return Ok(response);
        }

    }
}

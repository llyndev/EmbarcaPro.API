using EmbarcaPro.API.Dtos.Request;
using EmbarcaPro.API.Enums;
using EmbarcaPro.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmbarcaPro.API.Controller
{
    [ApiController]
    [Route("api/facility")]
    [Authorize]
    public class FacilityController(IFacilityService facilityService) : ControllerBase
    {

        [HttpPost]
        [Authorize(Roles = nameof(UserRole.Admin))]
        public async Task<IActionResult> AddFacility([FromBody] CreateFacilityRequest request)
        {
            var result = await facilityService.AddFacilityAsync(request);

            if (!result.Success)
            {
                return BadRequest(new { error = result.Message });
            }

            return StatusCode(StatusCodes.Status201Created, new { message = result.Message , facility = result.Data });
        }

    }
}

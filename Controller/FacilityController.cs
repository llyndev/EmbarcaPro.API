using EmbarcaPro.API.Dtos.Request;
using EmbarcaPro.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmbarcaPro.API.Controller
{
    public class FacilityController(FacilityService facilityService) : ControllerBase
    {

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> AddFacility([FromBody] CreateFacilityRequest request)
        {
            var (success, message, facility) = await facilityService.AddFacilityAsync(request);

            if (!success)
            {
                return BadRequest(new { error = message });
            }

            return StatusCode(StatusCodes.Status201Created, new { message, facility });
        }

    }
}

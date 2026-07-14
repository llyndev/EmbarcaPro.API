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
            var result = await facilityService.AddFacilityAsync(request);

            if (!result.Success)
            {
                return BadRequest(new { error = result.Message });
            }

            return StatusCode(StatusCodes.Status201Created, new { message = result.Message , facility = result.Data });
        }

    }
}

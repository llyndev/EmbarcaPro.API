using EmbarcaPro.API.Dtos.Request;
using EmbarcaPro.API.Enums;
using EmbarcaPro.API.Extensions;
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
        [Authorize(Roles = nameof(UserRole.Admin) + "," + nameof(UserRole.Suporte))]
        public async Task<IActionResult> AddFacility([FromBody] CreateFacilityRequest request)
        {
            var result = await facilityService.AddFacilityAsync(request);

            return result.ToActionResult(this, StatusCodes.Status201Created);
        }

    }
}

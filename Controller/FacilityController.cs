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

        [HttpGet]
        public async Task<IActionResult> GetAllFacilities()
        {
            var result = await facilityService.GetAllFacilitiesAsync();

            return Ok(result);
        }

        [HttpGet("cpnj")]
        public async Task<IActionResult> GetFacilityByCnpj(string cnpj)
        {
            var result = await facilityService.GetFacilityByCnpjAsnyc(cnpj);

            return result.ToActionResult(this);
        }

        [HttpGet("name")]
        public async Task<IActionResult> GetFacilitiesByName(string name)
        {

            var result = await facilityService.GetFacilitiesByNameAsync(name);

            return result.ToActionResult(this);
        }

    }
}

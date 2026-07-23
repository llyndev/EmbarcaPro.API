using EmbarcaPro.API.Common.Results;
using EmbarcaPro.API.Dtos.Request;
using EmbarcaPro.API.Enums;
using EmbarcaPro.API.Extensions;
using EmbarcaPro.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmbarcaPro.API.Controllers
{
    [ApiController]
    [Route("api/freights")]
    [Authorize]
    public class FreightController(IFreightService freightService) : ControllerBase
    {

        [HttpPost]
        [Authorize(Roles = nameof(UserRole.Admin))]
        public async Task<IActionResult> CreateFreight([FromBody] CreateFreightRequest request)
        {
            var result = await freightService.CreateFreightAsync(request);

            return result.ToActionResult(this, StatusCodes.Status201Created);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllFreightsAsync([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {

            var response = await freightService.GetAllFreightsAsync(page, pageSize);

            return response.ToActionResult(this);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetFreightById([FromRoute] int id)
        {

            var response = await freightService.GetFreightById(id);

            return response.ToActionResult(this);

        }

        [HttpPut("{id}/start")]
        public async Task<IActionResult> StartFreight(int id)
        {
            var response = await freightService.StartTripAsync(id);

            return response.ToActionResult(this);
        }

        [HttpPut("{id}/finish")]
        public async Task<IActionResult> FinishFreight(int id)
        {
            var response = await freightService.FinishTripAsync(id);

            return response.ToActionResult(this);
        }

        [HttpPut("{id}/cancel")]
        public async Task<IActionResult> CancelFreight(int id)
        {
            var response = await freightService.CancelTripAsyncs(id);

            return response.ToActionResult(this);
        }

    }
}

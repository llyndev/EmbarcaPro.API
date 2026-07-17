using EmbarcaPro.API.Common.Pagination;
using EmbarcaPro.API.Common.Results;
using EmbarcaPro.API.Dtos.Request;
using EmbarcaPro.API.Enums;
using EmbarcaPro.API.Extensions;
using EmbarcaPro.API.Models;
using EmbarcaPro.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmbarcaPro.API.Controller
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

            if (!result.Success)
            {
                return result.ErrorType switch
                {
                    ErrorType.NotFound => NotFound(new { error = result.Message }),
                    ErrorType.Validation => BadRequest(new { error = result.Message }),
                    ErrorType.Conflict => Conflict(new { error = result.Message }),
                    ErrorType.Unauthorized => Unauthorized(new { error = result.Message }),
                    ErrorType.Forbidden => StatusCode(StatusCodes.Status403Forbidden, new { error = result.Message }),
                    _ => BadRequest(new { error = result.Message })
                };
            }

            return StatusCode(StatusCodes.Status201Created, new { message = result.Message, freight = result.Data });
        }

        [HttpGet]
        public async Task<IActionResult> GetAllFreightsAsync([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {

            var response = await freightService.GetAllFreightsAsync(page, pageSize);

            return response.ToActionResult(this);
        }

    }
}

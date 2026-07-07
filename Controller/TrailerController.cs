using EmbarcaPro.API.Dtos.Request;
using EmbarcaPro.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmbarcaPro.API.Controller
{
    public class TrailerController(ITrailerService trailerService) : ControllerBase
    {
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddTrailer([FromBody] CreateTrailerRequest request)
        {
            var (success, message, trailer) = await trailerService.AddTrailerAsync(request);

            if (!success)
            {
                return BadRequest(new { error = message });
            }

            return StatusCode(StatusCodes.Status201Created, new { message, trailer });
        }

    }
}

using EmbarcaPro.API.Dtos.Request;
using EmbarcaPro.API.Enums;
using EmbarcaPro.API.Extensions;
using EmbarcaPro.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmbarcaPro.API.Controller
{
    [ApiController]
    [Route("/api/trailer")]
    [Authorize]
    public class TrailerController(ITrailerService trailerService) : ControllerBase
    {
        [HttpPost]
        [Authorize(Roles = nameof(UserRole.Admin) + "," + nameof(UserRole.Suporte))]
        public async Task<IActionResult> AddTrailer([FromBody] CreateTrailerRequest request)
        {
            var result = await trailerService.AddTrailerAsync(request);

            return result.ToActionResult(this, StatusCodes.Status201Created);
        }

    }
}

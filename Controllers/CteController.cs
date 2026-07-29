using EmbarcaPro.API.Dtos.Request;
using EmbarcaPro.API.Enums;
using EmbarcaPro.API.Extensions;
using EmbarcaPro.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmbarcaPro.API.Controllers
{
    [ApiController]
    [Route("api/ctes")]
    [Authorize]
    public class CteController(ICteService cteService) : ControllerBase
    {
        [HttpPost]
        [Authorize(Roles = nameof(UserRole.Admin) + "," + nameof(UserRole.Operacional))]
        public async Task<IActionResult> Create([FromBody] CreateCteRequest request)
        {
            var result = await cteService.CreateCteAsync(request);

            return result.ToActionResult(this, StatusCodes.Status201Created);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var result = await cteService.GetAllCtesAsync(page, pageSize);

            return result.ToActionResult(this);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var result = await cteService.GetCteByIdAsync(id);

            return result.ToActionResult(this);
        }

        [HttpPut("{id:int}/authorize")]
        [Authorize(Roles = nameof(UserRole.Admin))]
        public async Task<IActionResult> Authorize([FromRoute] int id)
        {
            var result = await cteService.AuthorizeCteAsync(id);

            return result.ToActionResult(this);
        }

        [HttpPut("{id:int}/cancel")]
        [Authorize(Roles = nameof(UserRole.Admin))]
        public async Task<IActionResult> Cancel([FromRoute] int id)
        {
            var result = await cteService.CancelCteAsync(id);

            return result.ToActionResult(this);
        }

        [HttpPut("{id:int}/deny")]
        [Authorize(Roles = nameof(UserRole.Admin))]
        public async Task<IActionResult> Deny([FromRoute] int id)
        {
            var result = await cteService.DenyCteAsync(id);

            return result.ToActionResult(this);
        }
    }
}

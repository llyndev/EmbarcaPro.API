using System.Text;
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

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)   
        {
            var result = await cteService.GetCteByPublicIdAsync(id);

            return result.ToActionResult(this);
        }

        [HttpPut("{id:guid}/authorize")]
        [Authorize(Roles = nameof(UserRole.Admin))]
        public async Task<IActionResult> Authorize([FromRoute] Guid id)
        {
            var result = await cteService.AuthorizeCteAsync(id);

            return result.ToActionResult(this);
        }

        [HttpPut("{id:guid}/cancel")]
        [Authorize(Roles = nameof(UserRole.Admin))]
        public async Task<IActionResult> Cancel([FromRoute] Guid id)
        {
            var result = await cteService.CancelCteAsync(id);

            return result.ToActionResult(this);
        }

        [HttpPut("{id:guid}/deny")]
        [Authorize(Roles = nameof(UserRole.Admin))]
        public async Task<IActionResult> Deny([FromRoute] Guid id)
        {
            var result = await cteService.DenyCteAsync(id);

            return result.ToActionResult(this);
        }

        [HttpPut("{id:guid}/prepare")]
        [Authorize(Roles = nameof(UserRole.Admin) + "," + nameof(UserRole.Operacional))]
        public async Task<IActionResult> Prepare([FromRoute] Guid id)
        {
            var result = await cteService.PrepareForTransmissionAsync(id);
            return result.ToActionResult(this);
        }

        [HttpGet("{id:guid}/xml")]
        [Authorize(Roles = nameof(UserRole.Admin) + "," + nameof(UserRole.Operacional))]
        public async Task<IActionResult> GetXml([FromRoute] Guid id)
        {
            var result = await cteService.GenerateXmlPreviewAsync(id);

            if (!result.Success)
                return result.ToActionResult(this);

            return Content(result.Data!, "text/xml", Encoding.UTF8);
        }
    }
}

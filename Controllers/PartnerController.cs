using EmbarcaPro.API.Dtos.Request;
using EmbarcaPro.API.Enums;
using EmbarcaPro.API.Extensions;
using EmbarcaPro.API.Services;
using EmbarcaPro.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmbarcaPro.API.Controllers;

[ApiController]
[Route("api/partners")]
[Authorize]
public class PartnerController(IPartnerService partnerService) : Controller
{
    [HttpPost]
    [Authorize(Roles = nameof(UserRole.Admin) + "," + nameof(UserRole.Operacional))]
    public async Task<IActionResult> Create([FromBody] CreatePartnerRequest request)
    {
        var result = await partnerService.CreatePartnerAsync(request);
        return result.ToActionResult(this, StatusCodes.Status201Created);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] string? search = null)
    {
        var result = await partnerService.GetAllPartnersAsync(page, pageSize, search);
        return result.ToActionResult(this);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var result = await partnerService.GetPartnerByPublicIdAsync(id);
        return result.ToActionResult(this);
    }

    [HttpPut("{id:guid}/activate")]
    [Authorize(Roles = nameof(UserRole.Admin))]
    public async Task<IActionResult> Activate([FromRoute] Guid id)
    {
        var result = await partnerService.SetPartnerActiveAsync(id, active: true);
        return result.ToActionResult(this);
    }
}
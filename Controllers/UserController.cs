using EmbarcaPro.API.Dtos.Request;
using EmbarcaPro.API.Enums;
using EmbarcaPro.API.Extensions;
using EmbarcaPro.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmbarcaPro.API.Controllers
{
    [ApiController]
    [Route("api/users")]
    [Authorize]
    public class UserController(IUserService userService) : Controller
    {
        [HttpGet]
        public async Task<IActionResult> GetAllUserResponseAsync()
        {
            var result = await userService.GetAllUserResponseAsync();

            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetUserByIdResponseAsync([FromRoute] int id)
        {
            var result = await userService.GetUserByIdResponseAsync(id);

            return result.ToActionResult(this);
        }

        [HttpPut]
        [Authorize(Roles = nameof(UserRole.Admin) + "," + nameof(UserRole.Suporte))]
        public async Task<IActionResult> UpdateUserRoleAsync([FromBody] UpdateRoleRequest request)
        {

            var result = await userService.UpdateUserRoleAsync(request);

            return result.ToActionResult(this);

        }


    }
}

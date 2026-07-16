using EmbarcaPro.API.Dtos.Request;
using EmbarcaPro.API.Extensions;
using EmbarcaPro.API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EmbarcaPro.API.Controller
{
    [ApiController]
    [Route("/api/auth")]
    public class AuthController(IUserService userService) : ControllerBase
    {

        [HttpPost("register")] // api/auth/register
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            var result = await userService.RegisterUserAsync(request);

            return result.ToActionResult(
                this,
                StatusCodes.Status201Created);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var result = await userService.LoginAsync(request);

            return result.ToActionResult(
                this);

        }

    }
}

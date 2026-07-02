using EmbarcaPro.API.Dtos.Request;
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
            var (success, message) = await userService.RegisterUserAsync(request);

            if (!success)
            {
                return BadRequest(new { error = message });
            }

            return Ok(new { message });
        }

    }
}

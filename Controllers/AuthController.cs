using EmbarcaPro.API.Dtos.Request;
using EmbarcaPro.API.Enums;
using EmbarcaPro.API.Extensions;
using EmbarcaPro.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmbarcaPro.API.Controllers
{
    [ApiController]
    [Route("/api/auth")]
    [Authorize]
    public class AuthController(IUserService userService) : Controller
    {

        [HttpPost("onbord")]
        [AllowAnonymous]
        public async Task<IActionResult> OnboardAsync([FromBody] OnboardRequest request){ 

            var result = await userService.OnboardAsync(request);

            return result.ToActionResult(this, StatusCodes.Status201Created);
            
        }

        /// <summary>
        /// Adiciona um usuário a empresa do adm autenticado.
        /// </summary>
        [HttpPost("register")] // api/auth/register
        [Authorize(Roles = nameof(UserRole.Admin))]
        public async Task<IActionResult> RegisterUserAsync([FromBody] RegisterRequest request)
        {
            var result = await userService.RegisterUserAsync(request);

            return result.ToActionResult(this, StatusCodes.Status201Created);
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> LoginAsync([FromBody] LoginRequest request)
        {
            var result = await userService.LoginAsync(request);

            return result.ToActionResult(this);

        }

    }
}

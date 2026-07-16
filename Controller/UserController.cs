using EmbarcaPro.API.Common.Results;
using EmbarcaPro.API.Dtos.Request;
using EmbarcaPro.API.Dtos.Response;
using EmbarcaPro.API.Models;
using EmbarcaPro.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmbarcaPro.API.Controller
{
    [ApiController]
    [Route("api/users")]
    public class UserController(IUserService userService) : ControllerBase 
    {
        [HttpGet]
        public async Task<ActionResult<List<UserResponse>>> GetAllUserResponseAsync()
        {
            List<UserResponse> response = await userService.GetAllUserResponseAsync();

            return Ok(response);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<UserResponse>> GetUserByIdResponseAsync([FromRoute] int id)
        {
            var response = await userService.GetUserByIdResponseAsync(id);

            if (!response.Success) {
                return NotFound(new { error = response.Message });
            }

            return Ok(response);
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<User>> UpdateUserRoleAsync([FromBody] UpdateRoleRequest request)
        {

            var response = await userService.UpdateUserRoleAsync(request);

            if (!response.Success)
            { 
                return NotFound(new { error = response.Message });
            }

            return Ok(response);

        }


    }
}

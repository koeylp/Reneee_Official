using Microsoft.AspNetCore.Mvc;
using Reneee.Application.Contracts.Identity;
using Reneee.Application.DTOs.User;
using Reneee.Application.Models.Identity;

namespace Reneee.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IAuthService authService) : ControllerBase
    {
        private readonly IAuthService _authService = authService;

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register([FromBody] RegisterRequest registerRequest)
        {
            return Ok(await _authService.Register(registerRequest));
        }
    }
}

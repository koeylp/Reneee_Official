using Microsoft.AspNetCore.Mvc;
using Reneee.Application.DTOs.User;
using Reneee.Application.Services;

namespace Reneee.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(IUserService userService) : ControllerBase
    {
        private readonly IUserService _userService = userService;
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<UserDto>>> GetAllUsers()
        {
            return Ok(await _userService.GetAllUsers());
        }
    }
}

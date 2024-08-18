using Microsoft.AspNetCore.Mvc;
using Reneee.Application.DTOs.ResetPassword;
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

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetUserById([FromRoute] int id)
        {
            return Ok(await _userService.GetUserById(id));
        }

        [HttpPut("enable/{id}")]
        public async Task<ActionResult<UserDto>> EnableUser([FromRoute] int id)
        {
            return Ok(await _userService.EnableUser(id));
        }

        [HttpPut("disable/{id}")]
        public async Task<ActionResult<UserDto>> DisableUser([FromRoute] int id)
        {
            return Ok(await _userService.DisableUser(id));
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDto forgotPasswordDto)
        {
            await _userService.GeneratePasswordResetToken(forgotPasswordDto);
            return Ok("Password reset link has been sent to your email.");
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequestDto resetPasswordDto)
        {
            var result = await _userService.ResetPassword(resetPasswordDto);

            if (!result)
            {
                return BadRequest("Failed to reset password.");
            }

            return Ok("Password has been reset successfully.");
        }

    }

}

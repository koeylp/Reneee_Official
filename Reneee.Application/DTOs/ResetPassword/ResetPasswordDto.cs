using Reneee.Application.DTOs.User;

namespace Reneee.Application.DTOs.ResetPassword
{
    public class ResetPasswordDto
    {
        public int Id { get; set; }
        public string Token { get; set; }
        public UserDto User { get; set; }
    }
}

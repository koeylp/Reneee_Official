using Reneee.Application.DTOs.User;

namespace Reneee.Application.Contracts.Identity
{
    public interface IJwtTokenService
    {
        string GenerateAccessToken(UserDto user);
        string GenerateRefreshToken();
        string GenerateResetPasswordToken(UserDto user);
    }
}

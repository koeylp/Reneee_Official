using Reneee.Application.DTOs.User;
using Reneee.Application.Models.Identity;

namespace Reneee.Application.Contracts.Identity
{
    public interface IAuthService
    {
        Task<AuthResponse> Login(AuthRequest authRequest);
        Task<UserDto> Register(RegisterRequest registerRequest);
    }
}

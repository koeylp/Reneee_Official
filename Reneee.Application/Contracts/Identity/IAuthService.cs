using Reneee.Application.DTOs.User;
using Reneee.Application.Models.Identity;

namespace Reneee.Application.Contracts.Identity
{
    public interface IAuthService
    {
        Task<UserDto> Register(RegisterRequest registerRequest);
    }
}

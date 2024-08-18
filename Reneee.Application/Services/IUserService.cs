
using Reneee.Application.DTOs.ResetPassword;
using Reneee.Application.DTOs.User;
using Reneee.Domain.Entities;

namespace Reneee.Application.Services
{
    public interface IUserService
    {
        Task<UserDto> DisableUser(int id);
        Task<UserDto> EnableUser(int id);
        Task GeneratePasswordResetToken(ForgotPasswordDto forgotPasswordDto);
        Task<IReadOnlyList<UserDto>> GetAllUsers();
        Task<UserDto> GetUserById(int id);
        Task<User> GetUserFromEmailClaims();
        Task<bool> ResetPassword(ResetPasswordRequestDto resetPasswordDto);
    }
}


using Reneee.Application.DTOs.User;

namespace Reneee.Application.Services
{
    public interface IUserService
    {
        Task<UserDto> DisableUser(int id);
        Task<UserDto> EnableUser(int id);
        Task<IReadOnlyList<UserDto>> GetAllUsers();
        Task<UserDto> GetUserById(int id);
    }
}


using Reneee.Application.DTOs.User;

namespace Reneee.Application.Services
{
    public interface IUserService
    {
        Task<IReadOnlyList<UserDto>> GetAllUsers();
    }
}

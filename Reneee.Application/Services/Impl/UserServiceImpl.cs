using AutoMapper;
using Microsoft.Extensions.Logging;
using Reneee.Application.Contracts.Persistence;
using Reneee.Application.DTOs.User;

namespace Reneee.Application.Services.Impl
{
    public class UserServiceImpl(IUserRepository userRepository,
                                 IMapper mapper,
                                 ILogger logger) : IUserService
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IMapper _mapper = mapper;
        private readonly ILogger _logger = logger;
        public async Task<IReadOnlyList<UserDto>> GetAllUsers()
        {
            _logger.LogInformation("Fetching all users");
            return _mapper.Map<IReadOnlyList<UserDto>>(await _userRepository.GetAll());
        }
    }
}

using AutoMapper;
using Reneee.Application.Contracts.Identity;
using Reneee.Application.Contracts.Persistence;
using Reneee.Application.DTOs.User;
using Reneee.Application.Models.Identity;
using Reneee.Application.Utils;
using Reneee.Domain.Entities;

namespace Reneee.Identity.Services
{
    public class AuthService(IUnitOfWork unitOfWork, IMapper mapper, IUserRepository userRepository) : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;
        private readonly IUserRepository _userRepository = userRepository;
        public async Task<UserDto> Register(RegisterRequest registerRequest)
        {
            if (!DateOnly.TryParse(registerRequest.Dob, out var dob))
            {
                throw new ArgumentException("Invalid date format for Dob");
            }
            var hashedPassword = PasswordUtils.HashPassword(registerRequest.Password);
            var userEntity = new User
            {
                FirstName = registerRequest.FirstName,
                LastName = registerRequest.LastName,
                Email = registerRequest.Email,
                Password = hashedPassword,
                Gender = Enum.Parse<Gender>(registerRequest.Gender),
                Dob = dob,
                Role = Role.Customer,
                Status = 1 
            };
            var savedUser = await _unitOfWork.UserRepository.Add(userEntity);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<UserDto>(savedUser);
        }
    }
}

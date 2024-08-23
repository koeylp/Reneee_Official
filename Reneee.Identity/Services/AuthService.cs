using AutoMapper;
using Reneee.Application.Contracts.Identity;
using Reneee.Application.Contracts.Persistence;
using Reneee.Application.Contracts.ThirdService;
using Reneee.Application.DTOs.User;
using Reneee.Application.Exceptions;
using Reneee.Application.Models.Identity;
using Reneee.Application.Utils;
using Reneee.Domain.Entities;

namespace Reneee.Identity.Services
{
    public class AuthService(IUnitOfWork unitOfWork,
                             IMapper mapper,
                             IUserRepository userRepository,
                             ICacheService cacheService,
                             IJwtTokenService jwtTokenService) : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;
        private readonly IUserRepository _userRepository = userRepository;
        private readonly ICacheService _cacheService = cacheService;
        private readonly IJwtTokenService _jwtTokenService = jwtTokenService;

        public async Task<AuthResponse> Login(AuthRequest authRequest)
        {
            var foundUser = await _userRepository.GetByEmail(authRequest.Email);
            if (foundUser == null || !PasswordUtils.Verify(authRequest.Password, foundUser.Password))
            {
                throw new BadRequestException("Invalid email or password.");
            }
            string AccessToken = _jwtTokenService.GenerateAccessToken(_mapper.Map<UserDto>(foundUser));
            string RefreshToken = _jwtTokenService.GenerateRefreshToken();
            await _cacheService.SetAsync(foundUser.Email, AccessToken, TimeSpan.FromHours(1));
            await _unitOfWork.SaveChangesAsync();
            return new AuthResponse(AccessToken, RefreshToken);
        }

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
                Status = 1,
                CreatedAt = DateTime.Now,
            };
            var savedUser = await _unitOfWork.UserRepository.Add(userEntity);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<UserDto>(savedUser);
        }
    }
}

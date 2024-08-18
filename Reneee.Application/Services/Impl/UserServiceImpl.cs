using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Reneee.Application.Contracts.Identity;
using Reneee.Application.Contracts.Persistence;
using Reneee.Application.Contracts.ThirdService;
using Reneee.Application.DTOs.ResetPassword;
using Reneee.Application.DTOs.User;
using Reneee.Application.Exceptions;
using Reneee.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Reneee.Application.Services.Impl
{
    public class UserServiceImpl(IUserRepository userRepository,
                                 IMapper mapper,
                                 IHttpContextAccessor httpContextAccessor,
                                 IMailService mailService,
                                 IJwtTokenService jwtTokenService,
                                 IResetPasswordRepository resetPasswordRepository,
                                 IUnitOfWork unitOfWork,
                                 ILogger<UserServiceImpl> logger) : IUserService
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
        private readonly IMapper _mapper = mapper;
        private readonly IMailService _mailService = mailService;
        private readonly IJwtTokenService _jwtTokenService = jwtTokenService;
        private readonly IResetPasswordRepository _resetPasswordRepository = resetPasswordRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly ILogger _logger = logger;

        public async Task<UserDto> DisableUser(int id)
        {
            var userEntity = await _userRepository.Get(id)
                               ?? throw new NotFoundException("User not found");
            userEntity.Status = 0;
            await _userRepository.Update(userEntity);
            return _mapper.Map<UserDto>(userEntity);
        }

        public async Task<UserDto> EnableUser(int id)
        {
            var userEntity = await _userRepository.Get(id)
                              ?? throw new NotFoundException("User not found");
            userEntity.Status = 1;
            await _userRepository.Update(userEntity);
            return _mapper.Map<UserDto>(userEntity);
        }

        public async Task GeneratePasswordResetToken(ForgotPasswordDto forgotPassword)
        {
            var userEntity = await _userRepository.GetByEmail(forgotPassword.Email)
                ?? throw new NotFoundException("User not found with email " + forgotPassword.Email);
            var token = _jwtTokenService.GenerateResetPasswordToken(_mapper.Map<UserDto>(userEntity));
            var resetLink = $"http://localhost:5173/reset-password/{token}";

            bool sended = await _mailService.SendPasswordResetEmail(forgotPassword.Email, resetLink, userEntity.FirstName);
            if (sended)
            {
                var resetPasswordEntity = new ResetPassword
                {
                    Token = token,
                    User = userEntity,
                };
                await _resetPasswordRepository.Add(resetPasswordEntity);
                await _unitOfWork.SaveChangesAsync();
            }
        }

        public async Task<IReadOnlyList<UserDto>> GetAllUsers()
        {
            _logger.LogInformation("Fetching all users");
            return _mapper.Map<IReadOnlyList<UserDto>>(await _userRepository.GetAll());
        }

        public async Task<UserDto> GetUserById(int id)
        {
            _logger.LogInformation("Get user by id " + id);
            var userEntity = await _userRepository.Get(id)
                            ?? throw new NotFoundException("User not found");
            return _mapper.Map<UserDto>(userEntity);
        }

        public async Task<User> GetUserFromEmailClaims()
        {
            var user = _httpContextAccessor.HttpContext.User;
            var email = user.Claims.Where(a => a.Type == ClaimTypes.Email).FirstOrDefault().Value;
            return await _userRepository.GetByEmail(email);
        }

        public Task<bool> ResetPassword(ResetPasswordRequestDto resetPasswordDto)
        {
            throw new NotImplementedException();
        }

        //public async Task<bool> ResetPassword(ResetPasswordRequestDto resetPasswordDto)
        //{
        //    if (resetPasswordDto == null || string.IsNullOrEmpty(resetPasswordDto.Token))
        //    {
        //        throw new ArgumentException("Invalid token or data provided.");
        //    }

        //    var tokenHandler = new JwtSecurityTokenHandler();
        //    try
        //    {
        //        var tokenValidationParameters = new TokenValidationParameters
        //        {
        //            ValidateIssuerSigningKey = true,
        //            //IssuerSigningKey = new RsaSecurityKey(GetPublicKey()),
        //            ValidateIssuer = false,
        //            ValidateAudience = false,
        //            ClockSkew = TimeSpan.Zero,
        //            ValidateLifetime = true,
        //        };

        //        tokenHandler.ValidateToken(resetPasswordDto.Token, tokenValidationParameters, out var validatedToken);
        //        var jwtToken = validatedToken as JwtSecurityToken;

        //        // Check token expiry
        //        var expiryDateUnix = long.Parse(jwtToken.Claims.First(x => x.Type == JwtRegisteredClaimNames.Exp).Value);
        //        var expiryDateTimeUtc = DateTimeOffset.FromUnixTimeSeconds(expiryDateUnix).UtcDateTime;

        //        if (expiryDateTimeUtc < DateTime.UtcNow)
        //        {
        //            return false;
        //        }


        //        var isPasswordChanged = await UpdateUserPassword(resetPasswordDto.User.Id, "newPasswordHere");

        //        return isPasswordChanged;
        //    }
        //    catch (SecurityTokenException)
        //    {
        //        return false; 
        //    }
        //}
    }
}

﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Reneee.Application.Contracts.Persistence;
using Reneee.Application.DTOs.User;
using Reneee.Application.Exceptions;
using Reneee.Domain.Entities;
using System.Security.Claims;

namespace Reneee.Application.Services.Impl
{
    public class UserServiceImpl(IUserRepository userRepository,
                                 IMapper mapper,
                                 IHttpContextAccessor httpContextAccessor,
                                 ILogger<UserServiceImpl> logger) : IUserService
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
        private readonly IMapper _mapper = mapper;
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
    }
}

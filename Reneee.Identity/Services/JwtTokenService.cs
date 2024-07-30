using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Reneee.Application.Contracts.Identity;
using Reneee.Application.DTOs.User;
using Reneee.Application.Models.Identity;
using Reneee.Identity.Utils;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Reneee.Identity.Services
{
    public class JwtTokenService(IOptions<JwtSettings> jwtSettings) : IJwtTokenService
    {
        private readonly JwtSettings _jwtSettings = jwtSettings.Value;

        public string GenerateAccessToken(UserDto user)
        {
            var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Email, user.Email),
                new(ClaimTypes.Role, user.Role),
                new(ClaimTypes.DateOfBirth, user.Dob.ToString())

            };
            var _privateKey = RSAKeyUtils.GetPrivateKey(_jwtSettings.PrivateKeyPath);
            var credentials = new SigningCredentials(new RsaSecurityKey(_privateKey), SecurityAlgorithms.RsaSha256);
             
            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(_jwtSettings.AccessTokenExpirationMinutes),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string GenerateRefreshToken()
        {
            return Guid.NewGuid().ToString();
        }
    }
}

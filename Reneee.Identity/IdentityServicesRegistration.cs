using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Reneee.Application.Contracts.Identity;
using Reneee.Identity.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Reneee.Identity.Utils;
using Reneee.Application.Models.Identity;

namespace Reneee.Identity
{
    public static class IdentityServicesRegistration
    {
        public static IServiceCollection ConfigureIdentityServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));
            string _publicKeyPath = configuration["JwtSettings:PublicKeyPath"];
            string _privateKeyPath = configuration["JwtSettings:PrivateKeyPath"];
            RSAKeyUtils.EnsureRsaKeys(_privateKeyPath, _publicKeyPath);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
              {
                  options.TokenValidationParameters = new TokenValidationParameters
                  {
                      ValidateIssuer = true,
                      ValidateAudience = true,
                      ValidateLifetime = true,
                      ValidateIssuerSigningKey = true,
                      ValidIssuer = configuration["JwtSettings:Issuer"],
                      ValidAudience = configuration["JwtSettings:Audience"],
                      IssuerSigningKey = new RsaSecurityKey(RSAKeyUtils.GetPublicKey(_publicKeyPath))
                  };
              });

            /*            services.AddAuthorization(options =>
                        {
                            options.AddPolicy("AdminPolicy", policy => policy.RequireRole("Admin"));
                            options.AddPolicy("ManagerPolicy", policy => policy.RequireRole("Manager"));
                        });*/


            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IJwtTokenService, JwtTokenService>();


            return services;
        }
    }
}

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Reneee.Application.Contracts.Identity;
using Reneee.Identity.Services;

namespace Reneee.Identity
{
    public static class IdentityServicesRegistration
    {
        public static IServiceCollection ConfigureIdentityServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IAuthService, AuthService>();

            return services;
        }
    }
}

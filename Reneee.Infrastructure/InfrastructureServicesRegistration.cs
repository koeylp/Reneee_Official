using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Reneee.Infrastructure.Email;
using Reneee.Infrastructure.Payment;

namespace Reneee.Infrastructure
{
    public static class InfrastructureServicesRegistration
    {
        public static IServiceCollection ConfigureInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IEmailSender, EmailSender>();
            services.Configure<StripeSettings>(configuration.GetSection("StripeSettings"));
            return services;
        }
    }
}

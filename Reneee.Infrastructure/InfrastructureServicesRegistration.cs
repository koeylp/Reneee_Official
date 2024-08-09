using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Reneee.Application.Contracts.ThirdService;
using Reneee.Infrastructure.Email;
using Reneee.Infrastructure.GHN;
using Reneee.Infrastructure.Payment;

namespace Reneee.Infrastructure
{
    public static class InfrastructureServicesRegistration
    {
        public static IServiceCollection ConfigureInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<StripeSettings>(configuration.GetSection("StripeSettings"));
            services.Configure<GhnSettings>(configuration.GetSection("GhnSettings"));
            services.AddTransient<IEmailSender, EmailSender>();
            services.AddTransient<IStripePaymentService, StripePaymentService>();
            services.AddHttpClient<IGhnService, GhnApiService>();

            return services;
        }
    }
}

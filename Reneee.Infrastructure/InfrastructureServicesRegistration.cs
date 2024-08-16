using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Reneee.Application.Contracts.ThirdService;
using Reneee.Infrastructure.Email;
using Reneee.Infrastructure.GHN;
using Reneee.Infrastructure.Payment;
using Reneee.Infrastructure.Redis;
using StackExchange.Redis;

namespace Reneee.Infrastructure
{
    public static class InfrastructureServicesRegistration
    {
        public static IServiceCollection ConfigureInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<StripeSettings>(configuration.GetSection("StripeSettings"));
            services.Configure<GhnSettings>(configuration.GetSection("GhnSettings"));
            var redisSettings = configuration.GetSection("RedisSettings").Get<RedisSettings>();
            services.AddSingleton(redisSettings);
            services.AddSingleton<IConnectionMultiplexer>(sp =>
                ConnectionMultiplexer.Connect(configuration.GetConnectionString("RedisConnection")));
            services.Configure<MailSettings>(configuration.GetSection("MailSettings"));

            services.AddTransient<IMailService, MailService>();
            services.AddTransient<IStripePaymentService, StripePaymentService>();
            services.AddHttpClient<IGhnService, GhnApiService>();
            services.AddScoped<ICacheService, RedisCacheService>();

            return services;
        }
    }
}

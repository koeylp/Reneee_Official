using Microsoft.Extensions.DependencyInjection;
using Reneee.Application.Services.Impl;
using Reneee.Application.Services;
using System.Reflection;

namespace Reneee.Application
{
    public static class ApplicationServicesRegistration
    {
        public static IServiceCollection ConfigureApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddScoped<CategoryService, CategoryServiceImpl>();

            return services;
        }
    }
}

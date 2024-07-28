using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Reneee.Persistence.Extensions;

namespace Reneee.Persistence
{
    public static class PersistenceServiceRegistration
    {
        public static IServiceCollection ConfigurePersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.ConfigureOptions<DatabaseOptionSetup>();
            services.AddDbContext<ApplicationDbContext>(
                (serviceProvider, dbContextOptionBuilder) =>
                {
                    var databaseOptions = serviceProvider.GetService<IOptions<DatabaseOptions>>()!.Value;

                    if (databaseOptions.CommandTimeout <= 0)
                    {
                        throw new ArgumentException("CommandTimeout must be a positive number.");
                    }

                    dbContextOptionBuilder.UseSqlServer(databaseOptions.ConnectionString, sqlServerAction =>
                    {
                        sqlServerAction.MigrationsAssembly("Reneee.Persistence");
                        sqlServerAction.EnableRetryOnFailure(databaseOptions.MaxRetryCount);
                        sqlServerAction.CommandTimeout(databaseOptions.CommandTimeout);
                    });
                    dbContextOptionBuilder.EnableDetailedErrors(databaseOptions.EnableDetailedErrors);
                    dbContextOptionBuilder.EnableSensitiveDataLogging(databaseOptions.EnableSensitiveDataLogging);
                });

            return services;
        }
    }
}

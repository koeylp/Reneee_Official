using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Reneee.Persistence.Extensions;
using Reneee.Persistence.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Reneee.Application.Contracts.Persistence;

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
                    }).UseLazyLoadingProxies();
                    dbContextOptionBuilder.EnableDetailedErrors(databaseOptions.EnableDetailedErrors);
                    dbContextOptionBuilder.EnableSensitiveDataLogging(databaseOptions.EnableSensitiveDataLogging);
                });

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IAttributeRepository, AttributeRepository>();
            services.AddScoped<IAttributeValueRepository, AttributeValueRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IOrderDetailsRepository, OrderDetailsRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IPaymentRepository, PaymentRepository>();
            services.AddScoped<IProductAttributeRepository, ProductAttributeRepository>();
            services.AddScoped<IProductImageRepository, ProductImageRepository>();
            services.AddScoped<IProductPromotionRepository, ProductPromotionRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IPromotionRepository, PromotionRepository>();
            services.AddScoped<ITransactionRepository, TransactionRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IResetPasswordRepository, ResetPasswordRepository>();
            services.AddScoped<ISalesRepository, SalesRepository>();

            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();


            return services;
        }
    }
}

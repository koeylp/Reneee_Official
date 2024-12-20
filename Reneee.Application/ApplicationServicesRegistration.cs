﻿using Microsoft.Extensions.DependencyInjection;
using Reneee.Application.Services.Impl;
using Reneee.Application.Services;
using System.Reflection;
using Reneee.Application.Services.CronJobs;


namespace Reneee.Application
{
    public static class ApplicationServicesRegistration
    {
        public static IServiceCollection ConfigureApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddScoped<IAttributeService, AttributeServiceImpl>();
            services.AddScoped<ICategoryService, CategoryServiceImpl>();
            services.AddScoped<IOrderService, OrderServiceImpl>();
            services.AddScoped<IPaymentService, PaymentServiceImpl>();
            services.AddScoped<IProductService, ProductServiceImpl>();
            services.AddScoped<IPromotionService, PromotionServiceImpl>();
            services.AddScoped<ITransactionService, TransactionServiceImpl>();
            services.AddScoped<IUserService, UserServiceImpl>();
            services.AddScoped<IAttributeValueService, AttributeValueServiceImpl>();
            services.AddHostedService<PromotionPriceUpdaterService>();
            services.AddScoped<ISalesService, SalesServiceImpl>();
            services.AddScoped<IProvinceService, ProvinceServiceImpl>();
            //services.AddOrleansClient(builder =>
            //{
            //    builder.UseLocalhostClustering();
            //    builder.ConfigureApplicationParts(parts => parts.AddApplicationPart(typeof(OrderPaymentGrain).Assembly).WithReferences());
            //});
            return services;
        }
    }
}

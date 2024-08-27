using Microsoft.OpenApi.Models;
using Reneee.API.Loggers;
using Reneee.API.Middleware;
using Reneee.Application;
using Reneee.Identity;
using Reneee.Infrastructure;
using Reneee.Persistence;

namespace Reneee.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Logging.ClearProviders();
            builder.Logging.AddConsole();
            builder.Logging.AddDebug();
            builder.Logging.AddConfiguration(builder.Configuration.GetSection("Logging"));


            builder.Services.ConfigurePersistenceServices(builder.Configuration);
            builder.Services.ConfigureInfrastructureServices(builder.Configuration);
            builder.Services.ConfigureApplicationServices();
            builder.Services.ConfigureIdentityServices(builder.Configuration);
            builder.Services.AddHttpContextAccessor();
            builder.Logging.AddProvider(new FileLoggerProvider("Logs/app-log.txt"));

            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAllHeaders",
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                               .AllowAnyMethod()
                               .AllowAnyHeader();
                    });
            });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header using the Bearer scheme. 
                      Enter 'Bearer' [space] and then your token in the text input below.",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                  {
                    {
                      new OpenApiSecurityScheme
                      {
                        Reference = new OpenApiReference
                          {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                          },
                          Scheme = "oauth2",
                          Name = "Bearer",
                          In = ParameterLocation.Header,

                        },
                        new List<string>()
                      }
                    });

                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Reneee Api",

                });

            });


            var app = builder.Build();
            app.UseSwagger();
            app.UseSwaggerUI();
            // Configure the HTTP request pipeline.
            //if (app.Environment.IsDevelopment())
            //{

            //}

            app.UseCors("AllowAllHeaders");
            app.UseMiddleware<ExceptionMiddleware>();
            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();


            app.Run();
        }
    }
}


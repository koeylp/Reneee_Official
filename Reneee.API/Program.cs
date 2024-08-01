using Microsoft.OpenApi.Models;
using Reneee.API.Middleware;
using Reneee.Application;
using Reneee.Identity;
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

            builder.Services.ConfigureApplicationServices();
            builder.Services.ConfigureIdentityServices(builder.Configuration);
            // Add services to the container.
            builder.Services.AddControllers();
            
            builder.Services.AddCors(o =>
            {
                o.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header using the Bearer scheme. 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      Example: 'Bearer eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCJ9.eyJlbWFpbCI6InVzZXJAZXhhbXBsZS5jb20iLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJDdXN0b21lciIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL2RhdGVvZmJpcnRoIjoiMy80LzIwMDIiLCJleHAiOjE3MjI5OTYwMDIsImlzcyI6IlJlbmVlZSIsImF1ZCI6IlJlbmVlZUN1c3RvbWVyIn0.cCJ_7JWVMS7H1fKEAhqakcA3F_OgARfLFGKdX8DLPeD-yJJbIvMTURe2otBBfqlqrzKUHtpldzLw_Cbp8XzRj6w_AmoHAi_pBi5RelzCXxhf7EXHjGW4ST7WtyBYaiCIn-urswYamzVmihfa6ga0rtJIsjcLc4i4Anf44aQ2UEKNPojOtZCgh522n5hKT22CvMyGwZ2KUWCjUX-t3zlJ-IrbQWkho8J4LwCrTSpcljii2i2CkF9ttqAlgwbPQZe1BakHmhBG7vCqDXJHuj_KaO6HozTzDVOeR5jBzS9fMXfCAqbdItOln4NO-5d9Bv4pgDYa7ivYb_fFdsUcNfZUjw
                        + Staff :Bearer eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCJ9.eyJlbWFpbCI6InN0YWZmQGV4YW1wbGUuY29tIiwiaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy9yb2xlIjoiU3RhZmYiLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9kYXRlb2ZiaXJ0aCI6IjMvMi8yMDAyIiwiZXhwIjoxNzIyOTk2MTg4LCJpc3MiOiJSZW5lZWUiLCJhdWQiOiJSZW5lZWVDdXN0b21lciJ9.p8mPRrZVPJObsURTWRdhxSDvX0p878lSGQTI0xwBbSm7GGjCvJxqu_25XlPmeI3y40-n2B5XWOe6PpDtQHPcyUQ09RjBYwuSx4JC9px1XRlxzOUdYCK0HtQJL9IaBGyui3KOa5YLXkk3qTATz47EqvD41ILQIseew5xIKvor83RAWaSel9dxo_igUKKiwV0M_FFKxG8Sx139Iq0rFIM5Mkm8fX_Fi_OZIeTuH_5kDAeoQ7JbUZrxDU4KPo-Dws7bvLwOIWbmp4GGl_dUCivrJdsMDiyWiurO_HRUCGZUgfamJ1MHcl-bNAHEOz_oLzAZ8_xF4vjJqks50m5bADUfEg
'",
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

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseMiddleware<ExceptionMiddleware>();
            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();


            app.Run();
        }
    }
}


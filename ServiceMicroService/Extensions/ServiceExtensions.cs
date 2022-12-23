using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ProfilesMicroService.Api.Extensions;
using ServiceMicroService.Application.Services;
using ServiceMicroService.Application.Services.Abstractions;
using ServiceMicroService.Infrastructure;

namespace ServiceMicroService.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureJWTAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                   .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
                   {
                       options.Authority = configuration.GetValue<string>("Routes:AuthorityRoute") ?? throw new NotImplementedException();
                       options.Audience = configuration.GetValue<string>("Routes:Scopes") ?? throw new NotImplementedException();
                       options.TokenValidationParameters = new TokenValidationParameters
                       {
                           ValidateAudience = true,
                           ValidAudience = configuration.GetValue<string>("Routes:Scopes") ?? throw new NotImplementedException(),
                           ValidateIssuer = true,
                           ValidIssuer = configuration.GetValue<string>("Routes:AuthorityRoute") ?? throw new NotImplementedException(),
                           ValidateLifetime = true
                       };
                   });
        }
        public static void ConfigureDbConnection(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDBContext>(config =>
            {
                config.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("ServiceMicroService"));
            });
        }
        public static void ConfigureServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MappingProfile));
            services.AddScoped<IServiceService, ServiceService>();
            services.AddScoped<ISpecializationService, SpecializationService>();

            services.AddFluentValidationAutoValidation();
            services.AddFluentValidationClientsideAdapters();
            services.AddValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());
        }

        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(setup =>
            {
                setup.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Place to add JWT with Bearer",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                setup.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        { Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Name = "Bearer",
                        },
                        new List<string>()
                    }
                });

                //setup.IncludeXmlComments(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Comments.xml"));
            });
        }

    }
}

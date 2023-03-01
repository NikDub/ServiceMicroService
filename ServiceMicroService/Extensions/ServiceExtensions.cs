using FluentValidation;
using FluentValidation.AspNetCore;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ServiceMicroService.Application.Services;
using ServiceMicroService.Application.Services.Abstractions;
using ServiceMicroService.Infrastructure;
using ServiceMicroService.Infrastructure.Repository;
using ServiceMicroService.Infrastructure.Repository.Abstractions;

namespace ServiceMicroService.Extensions;

public static class ServiceExtensions
{
    public static void ConfigureJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                options.Authority = configuration.GetValue<string>("Routes:AuthorityRoute") ??
                                    throw new NotImplementedException();
                options.Audience = configuration.GetValue<string>("Routes:Scopes") ??
                                   throw new NotImplementedException();
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = true,
                    ValidAudience = configuration.GetValue<string>("Routes:Scopes") ??
                                    throw new NotImplementedException(),
                    ValidateIssuer = true,
                    ValidIssuer = configuration.GetValue<string>("Routes:AuthorityRoute") ??
                                  throw new NotImplementedException(),
                    ValidateLifetime = true
                };
            });
    }

    public static void ConfigureDbConnection(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(config =>
        {
            config.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly("ServiceMicroService"));
        });
    }

    public static void ConfigureServices(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(MappingProfile));
        services.AddScoped<IServiceService, ServiceService>();
        services.AddScoped<ISpecializationService, SpecializationService>();
        services.AddScoped<IServiceRepository, ServiceRepository>();
        services.AddScoped<ISpecializationRepository, SpecializationRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddFluentValidationAutoValidation();
        services.AddFluentValidationClientsideAdapters();
        services.AddValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());
    }
    public static void ConfigureMassTransit(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMassTransit(r =>
        {
            r.UsingRabbitMq((_, cfg) =>
            {
                cfg.Host(new Uri(configuration.GetValue<string>("RabbitMQ:ConnectionStrings") ??
                                 throw new NotImplementedException()));
            });
        });
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

            setup.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        },
                        Name = "Bearer"
                    },
                    new List<string>()
                }
            });
        });
    }
}
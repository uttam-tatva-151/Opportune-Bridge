using System.Text;
using Core.Beans;
using Core.Beans.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Repositories;

namespace WebApi.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Bind Configuration from app-json-setting file to strongly-typed classes
        IConfigurationSection jwtConfigSection = configuration.GetSection(Constants.JWT_CONFIG);
        services.Configure<JwtConfig>(jwtConfigSection);
        services.Configure<EmailSettings>(configuration.GetSection(Constants.EMAIL_CONFIG));
        services.Configure<RouteSettings>(configuration.GetSection(Constants.DEFAULT_ROUTE_CONFIG));

        // Register DbContext with connection string from appsettings.json
        string? databaseConnectionString = configuration.GetConnectionString(Constants.DATABASE_DEFAULT_CONNECTION);
        if (string.IsNullOrWhiteSpace(databaseConnectionString))
            throw new InvalidOperationException(Constants.ERROR_MISSING_DB_CONNECTION);

        // Get JwtConfig values and guard against nulls
        JwtConfig? jwtConfig = jwtConfigSection.Get<JwtConfig>() ?? throw new InvalidOperationException(Constants.ERROR_MISSING_JWT_SECTION);
        if (string.IsNullOrWhiteSpace(jwtConfig.Key) || string.IsNullOrWhiteSpace(jwtConfig.Issuer) || string.IsNullOrWhiteSpace(jwtConfig.Audience))
            throw new InvalidOperationException(Constants.ERROR_INVALID_JWT_VALUES);

        services.AddDbContext<AppDbContext>(options => options.UseNpgsql(databaseConnectionString));

        services.AddSession(options =>
        {
            options.IdleTimeout = TimeSpan.FromHours(Constants.SESSION_IDLE_TIME_OUT_HOURS);
            options.Cookie.HttpOnly = true;
            options.Cookie.IsEssential = true;
        });


        // JWT Authentication
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtConfig.Issuer,
                ValidAudience = jwtConfig.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig.Key))
            };
        });

        services.AddAuthorization();

        // Swagger JWT Security
        services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition(Constants.SWAGGER_SECURITY_SCHEME, new OpenApiSecurityScheme
                {
                    Description = Constants.SWAGGER_SECURITY_DESCRIPTION,
                    Name = Constants.SWAGGER_SECURITY_NAME,
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = Constants.SWAGGER_SECURITY_SCHEME_TYPE,
                    BearerFormat = Constants.SWAGGER_SECURITY_BEARER_FORMAT
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = Constants.SWAGGER_SECURITY_SCHEME
                            },
                            Scheme = Constants.SWAGGER_SECURITY_SCHEME_TYPE,
                            Name = Constants.SWAGGER_SECURITY_SCHEME,
                            In = ParameterLocation.Header
                        },
                        Array.Empty<string>()
                    }
                });
            });

        return services;
    }

}

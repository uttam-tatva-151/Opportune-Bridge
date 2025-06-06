using Core.Beans;
using Microsoft.OpenApi.Models;

namespace WebApi.Extensions;

/// <summary>
/// Provides extension methods for configuring Swagger documentation for the application.
/// </summary>
public static class SwaggerExtensions
{
    /// <summary>
    /// Configures Swagger documentation and security definitions for the application.
    /// </summary>
    /// <param name="services">The IServiceCollection instance to add Swagger services to.</param>
    /// <returns>The updated IServiceCollection instance.</returns>
    /// <remarks>
    /// This method performs the following:
    /// - Adds Swagger documentation generation.
    /// - Configures security definitions for JWT-based authentication.
    /// Ensure that the security scheme matches the application's authentication setup.
    /// </remarks>
    public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
    {
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
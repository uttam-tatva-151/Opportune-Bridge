using Services.Interfaces;
using Services.Services;

namespace WebApi.Extensions;

/// <summary>
/// Provides extension methods for registering application services.
/// </summary>
public static class ServiceExtensions
{
    /// <summary>
    /// Registers application services used in the business logic layer.
    /// </summary>
    /// <param name="services">The IServiceCollection instance to add application services to.</param>
    /// <returns>The updated IServiceCollection instance.</returns>
    /// <remarks>
    /// This method registers the following services:
    /// - IErrorLogService with ErrorLogService
    /// - IUserService with UserService
    /// - IJWTService with JWTService
    /// - ILoginService with LoginService
    /// Ensure that these services are properly implemented and follow the application's business logic patterns.
    /// </remarks>
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IErrorLogService, ErrorLogService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IJWTService, JWTService>();
        services.AddScoped<ILoginService, LoginService>();
        return services;
    }
}
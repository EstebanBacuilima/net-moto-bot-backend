using net_moto_bot.Application.Interfaces.Custom;
using net_moto_bot.Application.Interfaces.Public;
using net_moto_bot.Application.Services.Custom;
using net_moto_bot.Application.Services.Public;

namespace net_moto_bot.API.Extensions;

public static class DependencyInjectionService
{
    // Services
    public static IServiceCollection AddAppServices(this IServiceCollection services)
    {
        // Singleton
        services.AddSingleton<IJWTService, JWTService>();

        // Public schema
        services.AddScoped<IUserService, UserService>();

        return services;
    }
}

using net_moto_bot.Application.Interfaces.Custom;
using net_moto_bot.Application.Interfaces.Public;
using net_moto_bot.Application.Services;
using net_moto_bot.Application.Services.Custom;
using net_moto_bot.Application.Services.Public;
using net_moto_bot.Domain.Interfaces.Public;
using net_moto_bot.Infrastructure.Repositories.Public;

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

        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<IRoleService, RoleService>();

        return services;
    }
}

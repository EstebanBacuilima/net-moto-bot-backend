using net_moto_bot.Domain.Interfaces.Public;
using net_moto_bot.Infrastructure.Repositories.Public;

namespace net_moto_bot.API.Extensions;

public static class DependencyInjectionServiceRepository
{
    // Repositories
    public static IServiceCollection AddAppRespositories(this IServiceCollection services)
    {
        // Public schema
        services.AddScoped<IUserRepository, UserRepository>();

        return services;
    }
}

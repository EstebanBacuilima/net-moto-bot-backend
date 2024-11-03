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
        services.AddScoped<IAppointmentRepository, AppointmentRepository>();
        services.AddScoped<IBrandRepository, BrandRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<IEmployeeRepository, EmployeeRepository>();
        services.AddScoped<IEstablishmentRepository, EstablishmentRepository>();
        services.AddScoped<IPersonRepository, PersonRepository>();

        
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IRoleRepository, RoleRepository>();

        return services;
    }
}

using AutoMapper;
using net_moto_bot.Application.Interfaces.Custom;
using net_moto_bot.Application.Interfaces.Public;
using net_moto_bot.Application.Mappers;
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
        // Add mapper configuration.
        var mapperConfig = new MapperConfiguration(mc =>
        {
            mc.AddProfile(new MappingProfile());
        });
        IMapper mapper = mapperConfig.CreateMapper();
        services.AddSingleton(mapper);

        // Singleton
        services.AddSingleton<IJWTService, JWTService>();
        services.AddSingleton<IUploadFileService, UploadFileService>();

        // Public schema
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<ICustomerService, CustomerService>();
        services.AddScoped<IEmployeeService, EmployeeService>();
        services.AddScoped<IServiceService, ServiceService>();
        services.AddScoped<IBrandService, BrandService>();
        services.AddScoped<IEstablishmentService, EstablishmentService>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<IProductFileService, ProductFileService>();
        services.AddScoped<IRoleService, RoleService>();
        services.AddScoped<IUserChatService, UserChatService>();


        return services;
    }
}

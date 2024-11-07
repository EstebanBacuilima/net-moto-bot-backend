
using net_moto_bot.Application.Interfaces.Public;
using net_moto_bot.Domain.Entities;
using net_moto_bot.Domain.Interfaces.Public;

namespace net_moto_bot.Application.Services.Public;

public class UserRoleService(IUserRoleRepository _repository) : IUserRoleService
{
    public Task<UserRole> CreateAsync(UserRole userRole)
    {
        return _repository.SaveAsync(userRole);

    }
}

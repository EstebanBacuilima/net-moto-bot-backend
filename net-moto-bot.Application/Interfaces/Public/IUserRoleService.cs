using net_moto_bot.Domain.Entities;

namespace net_moto_bot.Application.Interfaces.Public;

public interface IUserRoleService
{
    public Task<UserRole> CreateAsync(UserRole userRole);

}

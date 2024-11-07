
using net_moto_bot.Domain.Entities;

namespace net_moto_bot.Domain.Interfaces.Public;

public interface IUserRoleRepository
{
    public Task<UserRole> SaveAsync(UserRole userRole);
}

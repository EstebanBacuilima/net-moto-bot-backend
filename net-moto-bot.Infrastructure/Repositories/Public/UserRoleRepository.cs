

using net_moto_bot.Domain.Entities;
using net_moto_bot.Domain.Interfaces.Public;
using net_moto_bot.Infrastructure.Connections;

namespace net_moto_bot.Infrastructure.Repositories.Public;

public class UserRoleRepository (PostgreSQLContext _context): IUserRoleRepository
{
    public async Task<UserRole> SaveAsync(UserRole userRole) 
    {
        await _context.AddAsync(userRole);
        await _context.SaveChangesAsync();
        return userRole;
    }
}

using Microsoft.EntityFrameworkCore;
using net_moto_bot.Domain.Entities;
using net_moto_bot.Domain.Interfaces.Public;
using net_moto_bot.Infrastructure.Connections;

namespace net_moto_bot.Infrastructure.Repositories.Public;

public class RoleRepository(PostgreSQLContext _context) : IRoleRepository
{
    public Task<List<Role>> FindAllAsync()
    {
        return _context.Roles.AsNoTrackingWithIdentityResolution().ToListAsync();
    }
}
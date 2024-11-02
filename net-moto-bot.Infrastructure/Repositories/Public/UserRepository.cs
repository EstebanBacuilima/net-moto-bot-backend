using Microsoft.EntityFrameworkCore;
using net_moto_bot.Domain.Entities;
using net_moto_bot.Domain.Interfaces.Public;
using net_moto_bot.Infrastructure.Connectoins;

namespace net_moto_bot.Infrastructure.Repositories.Public;

public class UserRepository (PostgreSQLContext _context) : IUserRepository
{
    public Task<User?> FindByEmailAsync(string email) 
    {
        return _context.Users
            .AsNoTracking()
            .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
            .FirstOrDefaultAsync(u => u.Email.Equals(email));
    }

    public Task<List<User>> FindAllAsync() 
    {
        return _context.Users.AsNoTracking().ToListAsync();
    }
}

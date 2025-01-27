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

    public Task<User?> FindByCodeAsync(string code) 
    {
        return _context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Code.Equals(code));
    }
    public Task<User?> FindByIdAsync(long id)
    {
        return _context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Id == id);
    }

    public Task<List<User>> FindAllAsync() 
    {
        return _context.Users.AsNoTracking().ToListAsync();
    }

    public Task<bool> ExistsByIdAsync(long id) 
    {
        return _context.Users.AsNoTracking().AnyAsync(u => u.Id == id) ;
    }
}

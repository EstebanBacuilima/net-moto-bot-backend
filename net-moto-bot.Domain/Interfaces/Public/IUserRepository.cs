using net_moto_bot.Domain.Entities;

namespace net_moto_bot.Domain.Interfaces.Public;

public interface IUserRepository
{
    public Task<User?> FindByEmailAsync(string email);
    public Task<User?> FindByCodeAsync(string code);
    public Task<User?> FindByIdAsync(long id);
    public Task<List<User>> FindAllAsync();
}

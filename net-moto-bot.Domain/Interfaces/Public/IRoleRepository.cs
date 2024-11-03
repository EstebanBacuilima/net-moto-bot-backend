
using net_moto_bot.Domain.Entities;

namespace net_moto_bot.Domain.Interfaces.Public;

public interface IRoleRepository
{
    public Task<List<Role>> FindAllAsync();
}

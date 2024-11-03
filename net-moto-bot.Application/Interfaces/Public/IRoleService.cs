
using net_moto_bot.Domain.Entities;

namespace net_moto_bot.Application.Interfaces.Public;

public interface IRoleService
{
    public Task<List<Role>> GetAllAsync();
}

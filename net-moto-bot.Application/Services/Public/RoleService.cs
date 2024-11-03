using net_moto_bot.Application.Interfaces.Public;
using net_moto_bot.Domain.Entities;
using net_moto_bot.Domain.Interfaces.Public;

namespace net_moto_bot.Application.Services.Public;

public class RoleService(IRoleRepository _repository) : IRoleService
{
    public Task<List<Role>> GetAllAsync()
    {
        return _repository.FindAllAsync();
    }
}

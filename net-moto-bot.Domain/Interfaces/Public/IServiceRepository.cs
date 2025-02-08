
using Service = net_moto_bot.Domain.Entities.Service;

namespace net_moto_bot.Domain.Interfaces.Public;

public interface IServiceRepository
{
    public Task<Service> SaveAsync(Service service);
    public Task<Service> UpdateAsync(Service service);
    public Task<List<Service>> FindAllAsync(string value);
    public Task<Service?> FindByIdAsync(int id);
    public Task ChangeStateAsync(int id, bool active);
    public Task<Service?> FindByCodeAsync(string code);
    public Task ChangeStateAsync(string code, bool active);
    public Task<List<Service>> FindAllActiveAsync(string value);
}

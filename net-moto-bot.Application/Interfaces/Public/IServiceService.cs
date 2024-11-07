
using net_moto_bot.Domain.Entities;
using Service = net_moto_bot.Domain.Entities.Service;

namespace net_moto_bot.Application.Interfaces.Public;

public interface IServiceService
{
    public Task<Service> CreateAsync(Service service);
    public Task<Service> UpdateAsync(Service service);
    public Task<List<Service>> GetAllAsync();
    public Task<Service?> GetByIdAsync(int id);
    public Task<Service> ChangeStateAsync(int id, bool active);
}

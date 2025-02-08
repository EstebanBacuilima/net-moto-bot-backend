
using Service = net_moto_bot.Domain.Entities.Service;

namespace net_moto_bot.Application.Interfaces.Public;

public interface IServiceService
{
    public Task<Service> CreateAsync(Service service);
    public Task<Service> UpdateAsync(Service service);
    public Task<List<Service>> GetAllAsync(string value);
    public Task<Service?> GetByIdAsync(int id);
    public Task<Service> ChangeStateAsync(string code, bool active);
    public Task<List<Service>> GetAllActiveAsync(string value);
}

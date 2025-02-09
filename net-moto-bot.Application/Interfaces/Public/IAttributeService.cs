using Attribute = net_moto_bot.Domain.Entities.Attribute;

namespace net_moto_bot.Application.Interfaces.Public;

public interface IAttributeService
{
    public Task<Attribute> CreateAsync(Attribute service);
    public Task<Attribute> UpdateAsync(Attribute service);
    public Task<List<Attribute>> GetAllAsync(string value);
    public Task<Attribute?> GetByIdAsync(int id);
    public Task<Attribute> ChangeStateAsync(string code, bool active);
}

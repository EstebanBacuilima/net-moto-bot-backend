
using Attribute =  net_moto_bot.Domain.Entities.Attribute;

namespace net_moto_bot.Domain.Interfaces.Public;

public interface IAttributeRepository
{
    public Task<Attribute> SaveAsync(Attribute attribute);
    public Task<Attribute> UpdateAsync(Attribute attribute);
    public Task<List<Attribute>> FindAllAsync(string value);
    public Task<Attribute?> FindByIdAsync(int id);
    public Task ChangeStateAsync(int id, bool active);
    public Task<Attribute?> FindByCodeAsync(string code);
    public Task ChangeStateAsync(string code, bool active);
    public Attribute? FindByName(string name);
}

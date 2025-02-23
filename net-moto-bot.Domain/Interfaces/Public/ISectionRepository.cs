
using net_moto_bot.Domain.Entities;

namespace net_moto_bot.Domain.Interfaces.Public;

public interface ISectionRepository
{
    public Task<List<Section>> FindAllIncludingProductsAsync();
    public Task<Section> SaveAsync(Section section);
    public Task<Section> UpdateAsync(Section section);
    public Task<List<Section>> FindAllAsync(string value);
    public Task<Section?> FindByIdAsync(int id);
    public Task ChangeStateAsync(int id, bool active);
    public Task<Section?> FindByCodeAsync(string code);
    public Task ChangeStateAsync(string code, bool active);
    public Section? FindByName(string name);
    public bool ExistsById(short id);
}

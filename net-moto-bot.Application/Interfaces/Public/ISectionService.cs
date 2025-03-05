
using net_moto_bot.Domain.Entities;

namespace net_moto_bot.Application.Interfaces.Public;

public interface ISectionService
{
    public Task<Section> CreateAsync(Section section);
    public Task<Section> UpdateAsync(Section section);
    public Task<List<Section>> GetAllAsync(string value);
    public Task<Section?> GetByIdAsync(int id);
    public Task<Section> ChangeStateAsync(string code, bool active);
    public Task<List<Section>> GetAllIncludingProductsAsync();
    public int ProductQuantityBySection(short sectionId);
}

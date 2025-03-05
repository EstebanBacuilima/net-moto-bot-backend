
using net_moto_bot.Domain.Entities;

namespace net_moto_bot.Domain.Interfaces.Public;

public interface IProductSectionRepository
{
    public Task<List<ProductSection>> FindAllIncludingProductsAsync();

    public Task<List<ProductSection>> SaveRangeAsync(List<ProductSection> productSections);

    public Task DeleteAllBySectionIdAsync(short sectionId);

    public List<int> FindAllProductIdsBySection(short sectionId);

}

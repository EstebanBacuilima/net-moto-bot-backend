using net_moto_bot.Domain.Entities;

namespace net_moto_bot.Application.Interfaces.Public;

public interface IProductSectionService
{
    public Task<List<ProductSection>> BulkCreateAsync(List<int> productIds, short sectionId);

    public Task<List<ProductSection>> GetAllIncludingProductsAsync();
    public List<int> GetAllProductIdsBySection(short sectionId);
}

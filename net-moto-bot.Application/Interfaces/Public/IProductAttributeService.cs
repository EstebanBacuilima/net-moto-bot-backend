
using net_moto_bot.Domain.Entities;

namespace net_moto_bot.Application.Interfaces.Public;

public interface IProductAttributeService
{
    public Task<ProductAttribute> CreateAsync(ProductAttribute productAttribute);
    public Task<ProductAttribute> UpdateAsync(ProductAttribute productAttribute);
    public Task<List<ProductAttribute>> GetAllByProductIdAsync(int productId);
    public Task<ProductAttribute?> GetByProductIdAndAttibuteIdAsync(int productId, short attibuteId);
    public Task ChangeStateAsync(int productId, int attributeId, bool active);
}

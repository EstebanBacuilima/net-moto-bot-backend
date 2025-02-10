
using net_moto_bot.Domain.Entities;

namespace net_moto_bot.Domain.Interfaces.Public;

public interface IProductAttributeRepository
{
    public Task<ProductAttribute> SaveAsync(ProductAttribute productAttribute);
    public Task<ProductAttribute> UpdateAsync(ProductAttribute productAttribute);
    public Task<List<ProductAttribute>> FindAllByProductIdAsync(int productId, string value);
    public Task<ProductAttribute?> FindByProductIdAndAttibuteIdAsync(int productId, short attibuteId);
    public Task ChangeStateAsync(int productId, int attributeId, bool active);
    public bool ExistsByProductAndAttribute(int productid, short attributeId);
}

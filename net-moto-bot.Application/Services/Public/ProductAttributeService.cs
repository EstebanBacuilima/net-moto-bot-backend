using net_moto_bot.Application.Interfaces.Public;
using net_moto_bot.Domain.Entities;
using net_moto_bot.Domain.Enums.Custom;
using net_moto_bot.Domain.Exceptions.BadRequest;
using net_moto_bot.Domain.Interfaces.Public;

namespace net_moto_bot.Application.Services.Public;

public class ProductAttributeService(
    IProductAttributeRepository _repository) : IProductAttributeService
{
    public Task<ProductAttribute> CreateAsync(ProductAttribute productAttribute)
    {
        return _repository.SaveAsync(productAttribute);
    }

    public Task<ProductAttribute> UpdateAsync(ProductAttribute productAttribute)
    {
        bool exists = _repository.ExistsByProductAndAttribute(productAttribute.ProductId, productAttribute.AttributeId);

        if (!exists) throw new BadRequestException(ExceptionEnum.UserNotFound);

        return _repository.UpdateAsync(productAttribute);
    }

    public Task<List<ProductAttribute>> GetAllByProductIdAsync(int productId)
    {
        return _repository.FindAllByProductIdAsync(productId);
    }

    public Task<ProductAttribute?> GetByProductIdAndAttibuteIdAsync(int productId, short attibuteId)
    {
        return _repository.FindByProductIdAndAttibuteIdAsync(productId, attibuteId);
    }

    public async Task ChangeStateAsync(int productId, int attributeId, bool active)
    {
        await _repository.ChangeStateAsync(productId, attributeId, active);
    }
}

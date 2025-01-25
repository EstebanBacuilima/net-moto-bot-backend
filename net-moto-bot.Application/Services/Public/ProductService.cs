using net_moto_bot.Application.Interfaces.Public;
using net_moto_bot.Domain.Entities;
using net_moto_bot.Domain.Enums.Custom;
using net_moto_bot.Domain.Exceptions.BadRequest;
using net_moto_bot.Domain.Interfaces.Public;

namespace net_moto_bot.Application.Services.Public;

public class ProductService(IProductRepository _repository) : IProductService
{
    public Task<Product> CreateAsync(Product product)
    {
        return _repository.SaveAsync(product);
    }

    public async Task<Product> UpdateAsync(Product product)
    {
        Product? finded = await _repository.FindByCodeAsync(product.Code)
            ?? throw new BadRequestException(ExceptionEnum.EmailAlreadyExists);

        finded.BrandId = product.BrandId;
        finded.CategoryId = product.CategoryId;
        finded.Name = product.Name;
        finded.Description = product.Description;
        finded.Sku = product.Sku;

        return await _repository.UpdateAsync(finded);
    }

    public Task<List<Product>> GetAllAsync()
    {
        return _repository.FindAllAsync();
    }

    public Task<Product?> GetByIdAsync(int id)
    {
        return _repository.FindByIdAsync(id);
    }

    public async Task<Product> ChangeStateAsync(string code, bool active)
    {
        Product? product = await _repository.FindByCodeAsync(code)
            ?? throw new BadRequestException(ExceptionEnum.EmailAlreadyExists);

        product.Active = active;

        await _repository.ChangeStateAsync(code, active);

        return product;
    }

    public List<Product> GetAllByCategoryId(int categoryId)
    {
        return _repository.FindAllByCategoryId(categoryId);
    }
}

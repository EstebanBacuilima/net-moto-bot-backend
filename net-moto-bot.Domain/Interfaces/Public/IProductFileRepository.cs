using net_moto_bot.Domain.Entities;

namespace net_moto_bot.Domain.Interfaces.Public;

public interface IProductFileRepository
{
    public Task<List<ProductImage>> SaveRangeAsync(List<ProductImage> productFiles);

    public Task<ProductImage> SaveAsync(ProductImage productFile);

    public Task<List<ProductImage>> FindAllByProductIdAsync(int productId);

    public Task<List<ProductImage>> FindAllByProductCodeAsync(string productCode);

    public void ChangeState(string code, bool state);

    public void DeleteByCode(string code);
}

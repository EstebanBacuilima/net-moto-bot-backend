using net_moto_bot.Domain.Entities;

namespace net_moto_bot.Domain.Interfaces.Public;

public interface IProductFileRepository
{
    public Task<List<ProductFile>> SaveRangeAsync(List<ProductFile> productFiles);

    public Task<ProductFile> SaveAsync(ProductFile productFile);

    public Task<List<ProductFile>> FindAllByProductIdAsync(int productId);

    public Task<List<ProductFile>> FindAllByProductCodeAsync(string productCode);

    public void ChangeState(int id, bool state);
}

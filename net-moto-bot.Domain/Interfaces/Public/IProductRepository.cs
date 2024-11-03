
using net_moto_bot.Domain.Entities;

namespace net_moto_bot.Domain.Interfaces.Public;

public interface IProductRepository
{
    public Task<Product> SaveAsync(Product product);
    public Task<Product> UpdateAsync(Product product);
    public Task<List<Product>> FindAllAsync();
    public Task<Product?> FindByIdAsync(int id);
    public Task ChangeStateAsync(int id, bool active);
}

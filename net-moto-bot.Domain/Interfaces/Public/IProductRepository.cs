
using net_moto_bot.Domain.Entities;

namespace net_moto_bot.Domain.Interfaces.Public;

public interface IProductRepository
{
    public Task<Product> SaveAsync(Product product);
    public Task<Product> UpdateAsync(Product product);
    public Task<List<Product>> FindAllAsync();
    public Task<List<Product>> FindAllItemsAsync();
    public Task<Product?> FindByIdAsync(int id);
    public Task<Product?> FindByCodeAsync(string code);
    public Task ChangeStateAsync(int id, bool active);
    public Task ChangeStateAsync(string code, bool active);
    public Task<bool> ExistsByNameAsync(string name);
    public List<Product> FindAllByCategoryId(int categoryId);
    public Product? FindByCode(string code);
}


using net_moto_bot.Domain.Entities;

namespace net_moto_bot.Application.Interfaces.Public;

public interface IProductService
{
    public Task<Product> CreateAsync(Product product);
    public Task<Product> UpdateAsync(Product product);
    public Task<List<Product>> GetAllAsync();
    public Task<Product?> GetByIdAsync(int id);
    public Task<Product> ChangeStateAsync(string code, bool active);
    public List<Product> GetAllByCategoryId(int categoryId);
}

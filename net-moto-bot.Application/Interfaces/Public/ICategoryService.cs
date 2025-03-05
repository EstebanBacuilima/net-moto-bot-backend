using net_moto_bot.Domain.Entities;
namespace net_moto_bot.Application.Interfaces.Public;

public interface ICategoryService
{
    public Task<List<Category>> GetAllAsync();
    public Task<List<Category>> GetAllContainingProductsAsync();
    public Task<Category?> GetByCodeAsync(string code);
    public Task<bool> ExistsByNameAsync(string name);
    public Task<Category> CreateAsync(Category category);
    public Task<Category> UpdateAsync(Category category);
    public Task<Category> UpdateActveAsync(string code, bool active);
}

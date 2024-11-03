using net_moto_bot.Domain.Entities;

namespace net_moto_bot.Domain.Interfaces.Public;

public interface ICategoryRepository
{
    public Task<List<Category>> FindAllAsync();
    public Task<Category?> FindByCodeAsync(string code);
    public Task<bool> ExistsByNameAsync(string name);
    public Task<Category> SaveAsync(Category category);
    public Task<Category> UpdateAsync(Category category);
    public Task<Category> UpdateActveAsync(Category category);
}

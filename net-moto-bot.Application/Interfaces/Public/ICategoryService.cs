using net_moto_bot.Domain.Entities;

namespace net_moto_bot.Application.Interfaces.Public;

public interface ICategoryService
{
    public Task<List<Category>> GetAllAsync();
    public Task<Category?> GetByCodeAsync(string code);
    public Task<Category> CreateAsync(Category category);
    public Task<Category> UpdateAsync(Category category);
    public Task<Category> UpdateActveAsync(Category category);
}

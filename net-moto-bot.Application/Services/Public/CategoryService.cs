using net_moto_bot.Application.Interfaces.Public;
using net_moto_bot.Domain.Entities;
using net_moto_bot.Domain.Interfaces.Public;

namespace net_moto_bot.Application.Services.Public;

public class CategoryService(
    ICategoryRepository _repository
) : ICategoryService
{
    public Task<Category> CreateAsync(Category category)
    {
        return _repository.SaveAsync(category);
    }

    public Task<List<Category>> GetAllAsync()
    {
        return _repository.FindAllAsync();
    }

    public Task<Category?> GetByCodeAsync(string code)
    {
        return _repository.FindByCodeAsync(code);
    }

    public Task<Category> UpdateActveAsync(Category category)
    {
        return _repository.UpdateActveAsync(category);
    }

    public Task<Category> UpdateAsync(Category category)
    {
        return _repository.UpdateAsync(category);
    }
}

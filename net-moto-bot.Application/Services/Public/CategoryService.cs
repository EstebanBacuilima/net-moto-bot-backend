using net_moto_bot.Application.Interfaces.Public;
using net_moto_bot.Domain.Entities;
using net_moto_bot.Domain.Interfaces.Public;

namespace net_moto_bot.Application.Services.Public;

public class CategoryService(
    ICategoryRepository _repository) : ICategoryService
{
    public Task<List<Category>> GetAllAsync()
    {
        return _repository.FindAllAsync();
    }

    public Task<Category?> GetByCodeAsync(string code)
    {
        return _repository.FindByCodeAsync(code);
    }

    public Task<bool> ExistsByNameAsync(string name)
    {
        return _repository.ExistsByNameAsync(name);
    }

    public Task<Category> CreateAsync(Category category)
    {
        return _repository.SaveAsync(category);
    }

    public Task<Category> UpdateAsync(Category category)
    {
        return _repository.UpdateAsync(category);
    }

    public Task<Category> UpdateActveAsync(string code, bool active)
    {
        return _repository.UpdateActveAsync(new() { Code = code, Active = active });
    }
}

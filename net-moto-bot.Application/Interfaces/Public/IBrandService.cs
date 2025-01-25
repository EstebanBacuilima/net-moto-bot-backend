using net_moto_bot.Domain.Entities;

namespace net_moto_bot.Application.Interfaces.Public;

public interface IBrandService
{
    public Task<List<Brand>> GetAllAsync();
    public Task<Brand?> GetByCodeAsync(string code);
    public Task<bool> ExistsByNameAsync(string name);
    public Task<Brand> CreateAsync(Brand brand);
    public Task<Brand> UpdateAsync(Brand brand);
    public Task<Brand> UpdateActveAsync(string code , bool active);
}

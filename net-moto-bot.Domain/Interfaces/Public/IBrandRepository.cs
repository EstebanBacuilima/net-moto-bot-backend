using net_moto_bot.Domain.Entities;

namespace net_moto_bot.Domain.Interfaces.Public;

public interface IBrandRepository
{
    public Task<List<Brand>> FindAllAsync();
    public Task<Brand?> FindByCodeAsync(string code);
    public Task<bool> ExistsByNameAsync(string name);
    public Task<Brand> SaveAsync(Brand brand);
    public Task<Brand> UpdateAsync(Brand brand);
    public Task<Brand> UpdateActveAsync(Brand brand);
}

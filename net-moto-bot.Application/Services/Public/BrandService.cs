using net_moto_bot.Application.Interfaces.Public;
using net_moto_bot.Domain.Entities;
using net_moto_bot.Domain.Interfaces.Public;

namespace net_moto_bot.Application.Services.Public;

public class BrandService(
    IBrandRepository _repository
) : IBrandService
{
    public Task<Brand> CreateAsync(Brand brand)
    {
        return _repository.SaveAsync(brand);
    }

    public Task<List<Brand>> GetAllAsync()
    {
        return _repository.FindAllAsync();
    }

    public Task<Brand?> GetByCodeAsync(string code)
    {
        return _repository.FindByCodeAsync(code);
    }

    public Task<Brand> UpdateActveAsync(Brand brand)
    {
        return _repository.UpdateActveAsync(brand);
    }

    public Task<Brand> UpdateAsync(Brand brand)
    {
        return _repository.UpdateAsync(brand);
    }
}

﻿using net_moto_bot.Application.Interfaces.Public;
using net_moto_bot.Domain.Entities;
using net_moto_bot.Domain.Enums.Custom;
using net_moto_bot.Domain.Exceptions.BadRequest;
using net_moto_bot.Domain.Interfaces.Public;

namespace net_moto_bot.Application.Services.Public;

public class BrandService(
    IBrandRepository _repository) : IBrandService
{
    public Task<List<Brand>> GetAllAsync()
    {
        return _repository.FindAllAsync();
    }

    public Task<Brand?> GetByCodeAsync(string code)
    {
        return _repository.FindByCodeAsync(code);
    }

    public Task<bool> ExistsByNameAsync(string name)
    {
        return _repository.ExistsByNameAsync(name);
    }

    public async Task<Brand> CreateAsync(Brand brand)
    {
        if (await _repository.ExistsByNameAsync(brand.Name.Trim())) throw new BadRequestException(ExceptionEnum.NameIsAlreadyExists);
        return await _repository.SaveAsync(brand);
    }

    public Task<Brand> UpdateAsync(Brand brand)
    {
        return _repository.UpdateAsync(brand);
    }

    public Task<Brand> UpdateActveAsync(string code, bool active)
    {
        return _repository.UpdateActveAsync(new()
        {
            Code = code,
            Active = active
        });
    }
}

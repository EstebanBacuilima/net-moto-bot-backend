using Microsoft.EntityFrameworkCore;
using net_moto_bot.Domain.Entities;
using net_moto_bot.Domain.Interfaces.Public;
using net_moto_bot.Infrastructure.Connectoins;

namespace net_moto_bot.Infrastructure.Repositories.Public;

public class BrandRepository(
    PostgreSQLContext _context
) : IBrandRepository
{
    public Task<bool> ExistsByNameAsync(string name)
    {
        return _context.Brands
            .AsNoTracking()
            .AnyAsync(b => b.Name.ToUpper().Trim().Equals(name.Trim().ToUpper()));
    }

    public Task<List<Brand>> FindAllAsync()
    {
        return _context.Brands
            .AsNoTracking()
            .Where(b => b.Active)
            .ToListAsync();
    }

    public Task<Brand?> FindByCodeAsync(string code)
    {
        return _context.Brands
            .AsNoTracking()
            .FirstOrDefaultAsync(b => b.Code == code);
    }

    public async Task<Brand> SaveAsync(Brand brand)
    {
        await _context.Brands.AddAsync(brand);
        await _context.SaveChangesAsync();
        return brand;
    }

    public async Task<Brand> UpdateActveAsync(Brand brand)
    {
        var finded = await _context.Brands.FirstAsync(b => b.Code.Equals(brand.Code));
        finded.Active = brand.Active;
        await _context.SaveChangesAsync();
        return finded;
    }

    public async Task<Brand> UpdateAsync(Brand brand)
    {
        var finded = await _context.Brands.FirstAsync(b => b.Code.Equals(brand.Code));
        finded.Name = brand.Name;
        finded.Logo = brand.Logo;
        finded.Description = brand.Description;
        await _context.SaveChangesAsync();
        return finded;
    }
}

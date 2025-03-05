using Microsoft.EntityFrameworkCore;
using net_moto_bot.Domain.Entities;
using net_moto_bot.Domain.Interfaces.Public;
using net_moto_bot.Infrastructure.Connections;

namespace net_moto_bot.Infrastructure.Repositories.Public;

public class CategoryRepository(
    PostgreSQLContext _context
) : ICategoryRepository
{
    public Task<bool> ExistsByNameAsync(string name)
    {
        return _context.Categories
            .AsNoTracking()
            .AnyAsync(c => c.Name.ToUpper().Trim().Equals(name.Trim().ToUpper()));
    }

    public Task<List<Category>> FindAllAsync()
    {
        return _context.Categories
             .AsNoTracking()
             .ToListAsync();
    }

    public Task<List<Category>> FindAllContainingProductsAsync()
    {
        return _context.Categories
             .AsNoTracking()
             .Where(c => c.Active && c.Products.Any(p => p.Active))
             .ToListAsync();
    }

    public Task<Category?> FindByCodeAsync(string code)
    {
        return _context.Categories
             .AsNoTracking()
             .FirstOrDefaultAsync(c => c.Code == code);
    }

    public async Task<Category> SaveAsync(Category category)
    {
        await _context.Categories.AddAsync(category);
        await _context.SaveChangesAsync();
        return category;
    }

    public async Task<Category> UpdateActveAsync(Category category)
    {
        var finded = await _context.Categories.FirstAsync(c => c.Code.Equals(category.Code));
        finded.Active = category.Active;
        await _context.SaveChangesAsync();
        return finded;
    }

    public async Task<Category> UpdateAsync(Category category)
    {
        var finded = await _context.Categories.FirstAsync(c => c.Code.Equals(category.Code));

        finded.Name = category.Name;
        finded.Description = category.Description;
        await _context.SaveChangesAsync();
        return finded;
    }
}

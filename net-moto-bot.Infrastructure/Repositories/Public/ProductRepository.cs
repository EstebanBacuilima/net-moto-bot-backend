using Microsoft.EntityFrameworkCore;
using net_moto_bot.Domain.Entities;
using net_moto_bot.Domain.Interfaces.Public;
using net_moto_bot.Infrastructure.Connectoins;

namespace net_moto_bot.Infrastructure.Repositories.Public;
public class ProductRepository(PostgreSQLContext _context) : IProductRepository
{
    public async Task<Product> SaveAsync(Product product)
    {
        await _context.Products.AddAsync(product);
        await _context.SaveChangesAsync();
        return product;
    }

    public async Task<Product> UpdateAsync(Product product)
    {
        _context.Products.Update(product);
        await _context.SaveChangesAsync();
        return product;
    }

    public Task<List<Product>> FindAllAsync()
    {
        return _context.Products.AsNoTracking().ToListAsync();
    }

    public Task<Product?> FindByIdAsync(int id)
    {
        return _context.Products
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task ChangeStateAsync(int id, bool active)
    {
        var query = "UPDATE products SET active = {0} WHERE id = {1}";

        await _context.Database.ExecuteSqlRawAsync(query, active, id);
    }
}

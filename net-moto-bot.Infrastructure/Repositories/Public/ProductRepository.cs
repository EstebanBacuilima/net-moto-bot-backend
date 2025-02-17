using Microsoft.EntityFrameworkCore;
using net_moto_bot.Domain.Entities;
using net_moto_bot.Domain.Interfaces.Public;
using net_moto_bot.Infrastructure.Connections;

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
        return _context.Products
            .AsNoTracking()
            .Include(p => p.ProductImages)
            .Where(p => p.Active &&
                        p.ProductImages.Any(pf => pf.Active) &&
                        p.Category != null &&
                        p.Category.Active)
            .ToListAsync();
    }

    public Task<List<Product>> FindAllItemsAsync()
    {
        return _context.Products
            .AsNoTracking()
            .Include(p => p.ProductImages)
            .ToListAsync();

    }

    public Task<Product?> FindByIdAsync(int id)
    {
        return _context.Products
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public Task<Product?> FindByCodeAsync(string code)
    {
        return _context.Products
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Code.Equals(code));
    }

    public async Task ChangeStateAsync(int id, bool active)
    {
        var query = "UPDATE products SET active = {0} WHERE id = {1}";

        await _context.Database.ExecuteSqlRawAsync(query, active, id);
    }

    public async Task ChangeStateAsync(string code, bool active)
    {
        var query = "UPDATE products SET active = {0} WHERE code = {1}";

        await _context.Database.ExecuteSqlRawAsync(query, active, code);
    }

    public Task<bool> ExistsByNameAsync(string name)
    {
        return _context.Products.AnyAsync(p => p.Name.ToUpper().Trim().Equals(name.ToUpper().Trim()));
    }

    public List<Product> FindAllByCategoryId(int categoryId)
    {
        return [.. _context.Products.AsNoTracking()
            .Include(p => p.ProductImages)
            .Where(p => p.CategoryId == categoryId &&
                        p.Active &&
                        p.ProductImages.Any(pf => pf.Active) &&
                        p.Category != null &&
                        p.Category.Active )
            .Select(p => new Product(){
                Id = p.Id,
                CategoryId = p.CategoryId,
                BrandId = p.BrandId,
                Code = p.Code,
                Name = p.Name,
                Sku = p.Sku,
                Description = p.Description,
                Active = p.Active,
                CreationDate = p.CreationDate,
                UpdateDate = p.UpdateDate,
                ProductImages = p.ProductImages.Where(pf => pf.Active).ToList()
            })];
    }

    public List<Product> FindAllByCategoryCode(string categoryCode)
    {
        return [.. _context.Products.AsNoTracking()
            .Include(p => p.ProductImages)
            .Where(p => p.Category.Code.Equals(categoryCode) &&
                        p.Active &&
                        p.ProductImages.Any(pf => pf.Active) &&
                        p.Category != null &&
                        p.Category.Active )
            .Select(p => new Product(){
                Id = p.Id,
                CategoryId = p.CategoryId,
                BrandId = p.BrandId,
                Code = p.Code,
                Name = p.Name,
                Sku = p.Sku,
                Description = p.Description,
                Active = p.Active,
                CreationDate = p.CreationDate,
                UpdateDate = p.UpdateDate,
                ProductImages = p.ProductImages.Where(pf => pf.Active).ToList()
            })];
    }

    public Product? FindByCode(string code) 
    {
        return _context.Products.AsNoTracking()
            .Include(p => p.Category)
            .Include(p => p.Brand)
            .Include(p => p.ProductImages.Where(pi => pi.Active))
            .Include(p => p.ProductAttributes.Where(pa => pa.Active))
                .ThenInclude(pa => pa.Attribute)
            .Where(p => p.Code.Equals(code))
            .FirstOrDefault();
    }
}

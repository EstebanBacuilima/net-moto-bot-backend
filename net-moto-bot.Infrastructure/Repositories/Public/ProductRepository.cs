﻿using Microsoft.EntityFrameworkCore;
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
        return _context.Products
            .AsNoTracking()
            .Include(p => p.ProductFiles)
            .Where(p => p.Active &&
                        p.ProductFiles.Any(pf => pf.Active) &&
                        p.Category != null &&
                        p.Category.Active)
            .ToListAsync();
    }

    public Task<List<Product>> FindAllItemsAsync()
    {
        return _context.Products
            .AsNoTracking()
            .Include(p => p.ProductFiles)
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

    public List<Product> FindAllByCategoryId(int categoryId)
    {
        return [.. _context.Products.AsNoTracking()
            .Include(p => p.ProductFiles)
            .Where(p => p.CategoryId == categoryId &&
                        p.Active &&
                        p.ProductFiles.Any(pf => pf.Active) &&
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
                ProductFiles = p.ProductFiles.Where(pf => pf.Active).ToList()
            })];
    }
}


using Microsoft.EntityFrameworkCore;
using net_moto_bot.Domain.Entities;
using net_moto_bot.Domain.Exceptions.BadRequest;
using net_moto_bot.Domain.Interfaces.Public;
using net_moto_bot.Infrastructure.Connectoins;
using System;

namespace net_moto_bot.Infrastructure.Repositories.Public;

public class ProductFileRepository(PostgreSQLContext _context) : IProductFileRepository
{
    public async Task<List<ProductFile>> SaveRangeAsync(List<ProductFile> productFiles)
    {
        await _context.ProductFiles.AddRangeAsync(productFiles);
        await _context.SaveChangesAsync();
        return productFiles;
    }

    public async Task<ProductFile> SaveAsync(ProductFile productFile)
    {
        await _context.ProductFiles.AddAsync(productFile);
        await _context.SaveChangesAsync();
        return productFile;
    }

    public Task<List<ProductFile>> FindAllByProductIdAsync(int productId)
    {
        return _context.ProductFiles
            .AsNoTracking()
            .Where(pf => pf.ProductId == productId)
            .ToListAsync();
    }

    public Task<List<ProductFile>> FindAllByProductCodeAsync(string productCode)
    {
        return _context.ProductFiles
            .AsNoTracking()
            .Where(pf => pf.Product != null && pf.Product.Code.Equals(productCode))
            .ToListAsync();
    }

    public void ChangeState(string code, bool active)
    {
        string query = "UPDATE product_files SET active = {0} WHERE code = {1}";

         _context.Database.ExecuteSqlRawAsync(query, active, code);
    }

    public void DeleteByCode(string code) 
    {
        string query = "delete from product_files where code = {0}";

        _context.Database.ExecuteSqlRawAsync(query, code);
    }
}

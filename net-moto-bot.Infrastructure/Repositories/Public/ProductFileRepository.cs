
using Microsoft.EntityFrameworkCore;
using net_moto_bot.Domain.Entities;
using net_moto_bot.Domain.Exceptions.BadRequest;
using net_moto_bot.Domain.Interfaces.Public;
using net_moto_bot.Infrastructure.Connectoins;

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

    public void ChangeState(int id, bool state)
    {
        ProductFile finded = _context.ProductFiles
            .Where(pf => pf.Id == id)
            .FirstOrDefault()
            ?? throw new BadRequestException(Domain.Enums.Custom.ExceptionEnum.OperationNotAllowed);

        finded.Active = state;
        _context.SaveChanges();
    }
}

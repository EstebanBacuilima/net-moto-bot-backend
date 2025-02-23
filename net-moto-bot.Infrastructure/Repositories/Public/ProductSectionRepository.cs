using Microsoft.EntityFrameworkCore;
using net_moto_bot.Domain.Entities;
using net_moto_bot.Domain.Interfaces.Public;
using net_moto_bot.Infrastructure.Connections;

namespace net_moto_bot.Infrastructure.Repositories.Public;

public class ProductSectionRepository(
    PostgreSQLContext _context) : IProductSectionRepository
{
    public Task<List<ProductSection>> FindAllIncludingProductsAsync()
    {
        return _context.ProductSections
            .AsNoTracking()
            .Where(ps => ps.Active)
            .ToListAsync();

    }

    public async Task<List<ProductSection>> SaveRangeAsync(List<ProductSection> productSections)
    {
        await _context.AddRangeAsync(productSections);
        await _context.SaveChangesAsync();
        return productSections;
    }

    public async Task DeleteAllBySectionIdAsync(short sectionId)
    {
        await _context.ProductSections
            .Where(ps => ps.SectionId == sectionId)
            .ExecuteDeleteAsync();
    }
}

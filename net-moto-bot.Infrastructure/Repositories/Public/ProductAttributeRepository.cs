
using Microsoft.EntityFrameworkCore;
using net_moto_bot.Domain.Entities;
using net_moto_bot.Domain.Interfaces.Public;
using net_moto_bot.Infrastructure.Connections;

namespace net_moto_bot.Infrastructure.Repositories.Public;

public class ProductAttributeRepository(
    PostgreSQLContext _context) : IProductAttributeRepository
{
    public async Task<ProductAttribute> SaveAsync(ProductAttribute productAttribute)
    {
        await _context.ProductAttributes.AddAsync(productAttribute);
        await _context.SaveChangesAsync();
        return productAttribute;
    }

    public async Task<ProductAttribute> UpdateAsync(ProductAttribute productAttribute)
    {
        _context.ProductAttributes.Update(productAttribute);
        await _context.SaveChangesAsync();
        return productAttribute;
    }

    public Task<List<ProductAttribute>> FindAllByProductIdAsync(int productId, string value)
    {
        return _context.ProductAttributes.AsNoTracking()
            .Where(pa => pa.ProductId == productId && (string.IsNullOrWhiteSpace(value) ||
                         EF.Functions.Like(pa.Value.ToUpper(), $"%{value.ToUpper()}%") ||
                         EF.Functions.Like(pa.Attribute.Name.ToUpper(), $"%{value.ToUpper()}%")) 
                         )
            .Include(pa => pa.Attribute)
            .ToListAsync();
    }

    public Task<ProductAttribute?> FindByProductIdAndAttibuteIdAsync(int productId, short attributeId)
    {
        return _context.ProductAttributes
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.ProductId == productId && p.AttributeId == attributeId);
    }

    public async Task ChangeStateAsync(int productId, int attributeId, bool active)
    {
        var query = "UPDATE product_attributes SET active = {0} WHERE product_id = {1} and attribute_id = {2}";

        await _context.Database.ExecuteSqlRawAsync(query, active, productId, attributeId);
    }

    public bool ExistsByProductAndAttribute(int productid, short attributeId)
    {
        return _context.ProductAttributes.Any(pa => pa.ProductId == productid && pa.AttributeId == attributeId);
    }
}

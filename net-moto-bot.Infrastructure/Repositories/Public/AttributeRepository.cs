using Attribute = net_moto_bot.Domain.Entities.Attribute;
using net_moto_bot.Domain.Interfaces.Public;
using net_moto_bot.Infrastructure.Connections;
using Microsoft.EntityFrameworkCore;

namespace net_moto_bot.Infrastructure.Repositories.Public;

public class AttributeRepository(PostgreSQLContext _context) : IAttributeRepository
{
    public async Task<Attribute> SaveAsync(Attribute attribute)
    {
        await _context.Attributes.AddAsync(attribute);
        await _context.SaveChangesAsync();
        return attribute;
    }

    public async Task<Attribute> UpdateAsync(Attribute attribute)
    {
        _context.Attributes.Update(attribute);
        await _context.SaveChangesAsync();
        return attribute;
    }

    public Task<List<Attribute>> FindAllAsync(string value)
    {
        return _context.Attributes.AsNoTracking()
            .Where(s => string.IsNullOrWhiteSpace(value) ||
                        EF.Functions.Like(s.Name.ToUpper(), $"%{value.ToUpper()}%") ||
                        EF.Functions.Like(s.Description.ToUpper(), $"%{value.ToUpper()}%"))
            .ToListAsync();
    }

    public Task<Attribute?> FindByIdAsync(int id)
    {
        return _context.Attributes
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task ChangeStateAsync(int id, bool active)
    {
        var query = "UPDATE attributes SET active = {0} WHERE id = {1}";

        await _context.Database.ExecuteSqlRawAsync(query, active, id);
    }

    public Task<Attribute?> FindByCodeAsync(string code)
    {
        return _context.Attributes
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Code.Equals(code));
    }

    public async Task ChangeStateAsync(string code, bool active)
    {
        var query = "UPDATE attributes SET active = {0} WHERE code = {1}";

        await _context.Database.ExecuteSqlRawAsync(query, active, code);
    }

    public Attribute? FindByName(string name)
    {
        return _context.Attributes.AsNoTracking()
            .Where(a => a.Name.ToUpper().Equals(name.ToUpper()))
            .FirstOrDefault();
    }
}

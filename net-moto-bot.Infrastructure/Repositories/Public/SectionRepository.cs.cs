using Microsoft.EntityFrameworkCore;
using net_moto_bot.Domain.Entities;
using net_moto_bot.Domain.Interfaces.Public;
using net_moto_bot.Infrastructure.Connections;

namespace net_moto_bot.Infrastructure.Repositories.Public;

public class SectionRepository(PostgreSQLContext _context) : ISectionRepository
{
    public Task<List<Section>> FindAllIncludingProductsAsync()
    {
        return _context.Sections
            .AsNoTracking()
            .Include(s => s.ProductSections)
                .ThenInclude(ps => ps.Product)
                    .ThenInclude(p => p.Brand)
            .Include(s => s.ProductSections)
                .ThenInclude(ps => ps.Product)
                    .ThenInclude(p => p.ProductImages)
            .Where(s => s.Active && s.ProductSections.Any(ps => ps.Active))
            .Select(s => new Section()
            {
                Id = s.Id,
                Code = s.Code,
                Name = s.Name,
                Description = s.Description,
                Active = s.Active,
                EndDate = s.EndDate,
                Sequence = s.Sequence,
                ProductSections = s.ProductSections.Where(ps => ps.Product.Active).ToList()
            })
            .OrderBy(s => s.Sequence)
            .ToListAsync();

    }

    public async Task<Section> SaveAsync(Section section)
    {
        await _context.Sections.AddAsync(section);
        await _context.SaveChangesAsync();
        return section;
    }

    public async Task<Section> UpdateAsync(Section section)
    {
        _context.Sections.Update(section);
        await _context.SaveChangesAsync();
        return section;
    }

    public Task<List<Section>> FindAllAsync(string value)
    {
        return _context.Sections.AsNoTracking()
            .Where(s => string.IsNullOrWhiteSpace(value) ||
                        EF.Functions.Like(s.Name.ToUpper(), $"%{value.ToUpper()}%") ||
                        EF.Functions.Like(s.Description.ToUpper(), $"%{value.ToUpper()}%"))
            .Select(s => new Section()
            {
                Id = s.Id,
                Code = s.Code,
                Name = s.Name,
                Description = s.Description,
                Active = s.Active,
                EndDate = s.EndDate,
                Sequence = s.Sequence,
                TotalProduct = s.ProductSections.Count()
            })
            .OrderBy(s => s.Sequence)
            .ToListAsync();
    }

    public Task<Section?> FindByIdAsync(int id)
    {
        return _context.Sections
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task ChangeStateAsync(int id, bool active)
    {
        var query = "UPDATE sections SET active = {0} WHERE id = {1}";

        await _context.Database.ExecuteSqlRawAsync(query, active, id);
    }

    public Task<Section?> FindByCodeAsync(string code)
    {
        return _context.Sections
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Code.Equals(code));
    }

    public async Task ChangeStateAsync(string code, bool active)
    {
        var query = "UPDATE sections SET active = {0} WHERE code = {1}";

        await _context.Database.ExecuteSqlRawAsync(query, active, code);
    }

    public Section? FindByName(string name)
    {
        return _context.Sections.AsNoTracking()
            .Where(a => a.Name.ToUpper().Equals(name.ToUpper()))
            .FirstOrDefault();
    }

    public bool ExistsById(short id)
    {
        return _context.Sections.Any(s => s.Id == id);
    }

    public int ProductQuantityBySection(short sectionId)
    {
        return _context.Sections.AsNoTracking()
            .Where(s => s.Id == sectionId)
            .Select(s => s.ProductSections.Count())
            .FirstOrDefault();
    }
}

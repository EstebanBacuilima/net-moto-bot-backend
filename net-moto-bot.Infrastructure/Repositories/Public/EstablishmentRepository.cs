using Microsoft.EntityFrameworkCore;
using net_moto_bot.Domain.Entities;
using net_moto_bot.Domain.Interfaces.Public;
using net_moto_bot.Infrastructure.Connectoins;

namespace net_moto_bot.Infrastructure.Repositories.Public;

public class EstablishmentRepository(
    PostgreSQLContext _context
) : IEstablishmentRepository
{
    public Task<List<Establishment>> FindAllAsync(bool? active, string name = "", string description = "")
    {
        return _context.Establishments
          .AsNoTracking()
          .Where(e => (string.IsNullOrEmpty(name) || e.Name.ToUpper().Contains(name.ToUpper())) &&
                        (string.IsNullOrEmpty(description) || e.Description!.ToUpper().Contains(description.ToUpper())) && (!active.HasValue || e.Active == active.Value))
          .ToListAsync();
    }

    public Task<Establishment?> FindByCodeAsync(string code)
    {
        return _context.Establishments
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Code == code);
    }

    public async Task<Establishment> SaveAsync(Establishment establishment)
    {
        await _context.Establishments.AddAsync(establishment);
        await _context.SaveChangesAsync();
        return establishment;
    }

    public async Task<Establishment> UpdateActiveAsync(Establishment establishment)
    {
        var finded = await _context.Establishments.FirstAsync(c => c.Code.Equals(establishment.Code));
        finded.Active = establishment.Active;
        await _context.SaveChangesAsync();
        return finded;
    }

    public async Task<Establishment> UpdateAsync(Establishment establishment)
    {
        var finded = await _context.Establishments.FirstAsync(c => c.Code.Equals(establishment.Code));
        finded.Name = establishment.Name;
        finded.Description = establishment.Description;
        finded.Latitude = establishment.Latitude;
        finded.Longitude = establishment.Longitude;
        await _context.SaveChangesAsync();
        return finded;
    }
}

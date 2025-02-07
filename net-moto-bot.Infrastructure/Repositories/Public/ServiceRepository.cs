using Microsoft.EntityFrameworkCore;
using net_moto_bot.Domain.Entities;
using net_moto_bot.Domain.Interfaces.Public;
using net_moto_bot.Infrastructure.Connectoins;

namespace net_moto_bot.Infrastructure.Repositories.Public;

public class ServiceRepository(
    PostgreSQLContext _context) : IServiceRepository
{
    public async Task<Service> SaveAsync(Service service)
    {
        await _context.Services.AddAsync(service);
        await _context.SaveChangesAsync();
        return service;
    }

    public async Task<Service> UpdateAsync(Service service)
    {
        _context.Services.Update(service);
        await _context.SaveChangesAsync();
        return service;
    }

    public Task<List<Service>> FindAllAsync(string value)
    {
        return _context.Services.AsNoTracking()
            .Where(s => string.IsNullOrWhiteSpace(value) || 
                        EF.Functions.Like(s.Name.ToUpper(), $"%{value.ToUpper()}%") || 
                        EF.Functions.Like(s.Description.ToUpper(), $"%{value.ToUpper()}%"))
            .ToListAsync();
    }

    public Task<Service?> FindByIdAsync(int id)
    {
        return _context.Services
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public Task<Service?> FindByCodeAsync(string code)
    {
        return _context.Services
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Code.Equals(code));
    }

    public async Task ChangeStateAsync(string code, bool active)
    {
        var query = "UPDATE services SET active = {0} WHERE code = {1}";

        await _context.Database.ExecuteSqlRawAsync(query, active, code);
    }

    public async Task ChangeStateAsync(int id, bool active)
    {
        var query = "UPDATE services SET active = {0} WHERE id = {1}";

        await _context.Database.ExecuteSqlRawAsync(query, active, id);
    }
}


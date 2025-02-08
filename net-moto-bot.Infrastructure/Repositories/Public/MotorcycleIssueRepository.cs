

using Microsoft.EntityFrameworkCore;
using net_moto_bot.Domain.Entities;
using net_moto_bot.Domain.Interfaces.Public;
using net_moto_bot.Infrastructure.Connectoins;

namespace net_moto_bot.Infrastructure.Repositories.Public;

public class MotorcycleIssueRepository(PostgreSQLContext _context) : IMotorcycleIssueRepository
{
    public async Task<MotorcycleIssue> SaveAsync(MotorcycleIssue motorcycleIssue)
    {
        await _context.MotorcycleIssues.AddAsync(motorcycleIssue);
        await _context.SaveChangesAsync();
        return motorcycleIssue;
    }

    public async Task<MotorcycleIssue> UpdateAsync(MotorcycleIssue motorcycleIssue)
    {
        _context.MotorcycleIssues.Update(motorcycleIssue);
        await _context.SaveChangesAsync();
        return motorcycleIssue;
    }

    public Task<List<MotorcycleIssue>> FindAllAsync(string value)
    {
        return _context.MotorcycleIssues.AsNoTracking()
            .Where(mi => string.IsNullOrWhiteSpace(value) || 
                         EF.Functions.Like(mi.IssueDescription, $"%{value}%") ||
                         EF.Functions.Like(mi.PossibleCauses.ToUpper(), $"%{value.ToUpper()}%") ||
                         EF.Functions.Like(mi.SolutionSuggestion.ToUpper(), $"%{value.ToUpper()}%"))
            .ToListAsync();
    }

    public Task<MotorcycleIssue?> FindByIdAsync(int id)
    {
        return _context.MotorcycleIssues
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public Task<MotorcycleIssue?> FindByCodeAsync(string code)
    {
        return _context.MotorcycleIssues
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Code.Equals(code));
    }

    public async Task ChangeStateAsync(string code, bool active)
    {
        var query = "UPDATE motorcycle_issues SET active = {0} WHERE code = {1}";

        await _context.Database.ExecuteSqlRawAsync(query, active, code);
    }

    public async Task ChangeStateAsync(int id, bool active)
    {
        var query = "UPDATE motorcycle_issues SET active = {0} WHERE id = {1}";

        await _context.Database.ExecuteSqlRawAsync(query, active, id);
    }
}


using net_moto_bot.Domain.Entities;

namespace net_moto_bot.Application.Interfaces.Public;

public interface IMotorcycleIssueService
{
    public Task<MotorcycleIssue> CreateAsync(MotorcycleIssue motorcycleIssue);
    public Task<MotorcycleIssue> UpdateAsync(MotorcycleIssue motorcycleIssue);
    public Task<List<MotorcycleIssue>> GetAllAsync();
    public Task<MotorcycleIssue?> GetByIdAsync(int id);
    public Task<MotorcycleIssue> ChangeStateAsync(string code, bool active);
}

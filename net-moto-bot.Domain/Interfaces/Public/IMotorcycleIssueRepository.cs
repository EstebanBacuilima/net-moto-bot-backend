using net_moto_bot.Domain.Entities;

namespace net_moto_bot.Domain.Interfaces.Public;

public interface IMotorcycleIssueRepository
{
    public Task<MotorcycleIssue> SaveAsync(MotorcycleIssue motorcycleIssue);
    public Task<MotorcycleIssue> UpdateAsync(MotorcycleIssue motorcycleIssue);
    public Task<List<MotorcycleIssue>> FindAllAsync(string value);
    public Task<MotorcycleIssue?> FindByIdAsync(int id);
    public Task ChangeStateAsync(int id, bool active);
    public Task<MotorcycleIssue?> FindByCodeAsync(string code);
    public Task ChangeStateAsync(string code, bool active);
}

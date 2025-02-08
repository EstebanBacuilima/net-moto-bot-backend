using net_moto_bot.Application.Interfaces.Public;
using net_moto_bot.Domain.Entities;
using net_moto_bot.Domain.Enums.Custom;
using net_moto_bot.Domain.Exceptions.BadRequest;
using net_moto_bot.Domain.Interfaces.Public;

namespace net_moto_bot.Application.Services.Public;

public class MotorcycleIssueService(IMotorcycleIssueRepository _repository) : IMotorcycleIssueService
{
    public Task<MotorcycleIssue> CreateAsync(MotorcycleIssue motorcycleIssue)
    {
        return _repository.SaveAsync(motorcycleIssue);
    }

    public async Task<MotorcycleIssue> UpdateAsync(MotorcycleIssue motorcycleIssue)
    {
        MotorcycleIssue? finded = await _repository.FindByCodeAsync(motorcycleIssue.Code)
            ?? throw new BadRequestException(ExceptionEnum.EmailAlreadyExists);

        finded.IssueDescription = motorcycleIssue.IssueDescription;
        finded.PossibleCauses = motorcycleIssue.PossibleCauses;
        finded.SolutionSuggestion = motorcycleIssue.SolutionSuggestion;
        finded.SeverityLevel = motorcycleIssue.SeverityLevel;
        finded.Active = motorcycleIssue.Active;
        return await _repository.UpdateAsync(finded);
    }

    public Task<List<MotorcycleIssue>> GetAllAsync(string value)
    {
        return _repository.FindAllAsync(value);
    }

    public Task<MotorcycleIssue?> GetByIdAsync(int id)
    {
        return _repository.FindByIdAsync(id);
    }

    public async Task<MotorcycleIssue> ChangeStateAsync(string code, bool active)
    {
        MotorcycleIssue? motorcycleIssue = await _repository.FindByCodeAsync(code)
            ?? throw new BadRequestException(ExceptionEnum.EmailAlreadyExists);

        motorcycleIssue.Active = active;

        await _repository.ChangeStateAsync(code, active);

        return motorcycleIssue;
    }
}

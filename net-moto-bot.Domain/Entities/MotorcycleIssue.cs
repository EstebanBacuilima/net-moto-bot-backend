
namespace net_moto_bot.Domain.Entities;

public class MotorcycleIssue
{
    public int Id { get; set; }

    public string Code { get; set; } = string.Empty;

    public string? IssueDescription { get; set; }

    public string? PossibleCauses { get; set; }

    public string? SolutionSuggestion { get; set; }

    public int? SeverityLevel { get; set; }

    public bool Active { get; set; }
}

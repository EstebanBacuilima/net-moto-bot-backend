namespace net_moto_bot.Domain.Entities;

public partial class Appointment
{
    public int Id { get; set; }

    public int ServiceId { get; set; }

    public int EstablishmentId { get; set; }

    public int CustomerId { get; set; }

    public int EmployeeId { get; set; }

    public string Code { get; set; } = null!;

    public DateTime Date { get; set; }

    public string? Observation { get; set; }

    public bool Active { get; set; }

    public DateTime CreationDate { get; set; }

    public DateTime UpdateDate { get; set; }

    public virtual Customer Customer { get; set; } = null!;

    public virtual Employee Employee { get; set; } = null!;

    public virtual Establishment Establishment { get; set; } = null!;

    public virtual Service Service { get; set; } = null!;
}

namespace net_moto_bot.Domain.Entities;

public partial class Employee
{
    public int Id { get; set; }

    public int PersonId { get; set; }

    public long UserId { get; set; }

    public bool Active { get; set; }

    public string Code { get; set; } = null!;

    public DateTime CreationDate { get; set; }

    public DateTime UpdateDate { get; set; }

    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

    public virtual Person Person { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}

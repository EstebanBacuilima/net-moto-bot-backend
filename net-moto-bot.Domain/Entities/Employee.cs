using System.Text.Json.Serialization;

namespace net_moto_bot.Domain.Entities;

public partial class Employee
{
    public int Id { get; set; }

    public int PersonId { get; set; }

    public int UserId { get; set; }

    public bool Active { get; set; }

    public string Code { get; set; } = string.Empty;

    public DateTime CreationDate { get; set; }

    public DateTime UpdateDate { get; set; }

    [JsonIgnore]
    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

    public virtual Person? Person { get; set; }

    public virtual User? User { get; set; }
}

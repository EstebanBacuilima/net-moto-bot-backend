using System.Text.Json.Serialization;

namespace net_moto_bot.Domain.Entities;

public partial class Customer
{
    public int Id { get; set; }

    public int PersonId { get; set; }

    public long UserId { get; set; }

    public string Code { get; set; } = string.Empty;

    public bool Active { get; set; }

    public DateTime CreationDate { get; set; }

    public DateTime UpdateDate { get; set; }

    [JsonIgnore]
    public virtual ICollection<Appointment> Appointments { get; set; } = [];

    public virtual Person? Person { get; set; }
}

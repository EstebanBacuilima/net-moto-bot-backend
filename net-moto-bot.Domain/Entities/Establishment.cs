using System.Text.Json.Serialization;

namespace net_moto_bot.Domain.Entities;


public partial class Establishment
{
    public int Id { get; set; }

    public string Code { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    public bool Active { get; set; }

    public double Latitude { get; set; }

    public double Longitude { get; set; }

    public DateTime CreationDate { get; set; }

    public DateTime UpdateDate { get; set; }

    public string? MainStreet { get; set; }

    public string? SecondStreet { get; set; }

    [JsonIgnore]
    public virtual ICollection<Appointment> Appointments { get; set; } = [];
}

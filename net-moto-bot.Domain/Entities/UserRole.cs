using System.Text.Json.Serialization;

namespace net_moto_bot.Domain.Entities;

public partial class UserRole
{
    public long UserId { get; set; }

    public int RoleId { get; set; }

    public DateTime CreationDate { get; set; }

    [JsonIgnore]
    public virtual Role Role { get; set; } = null!;

    [JsonIgnore]
    public virtual User User { get; set; } = null!;
}

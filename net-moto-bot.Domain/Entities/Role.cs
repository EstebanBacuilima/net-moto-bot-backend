using System.Text.Json.Serialization;

namespace net_moto_bot.Domain.Entities;

public partial class Role
{
    public int Id { get; set; }

    public string Code { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    public bool Active { get; set; }

    public DateTime CreationDate { get; set; }

    public DateTime UpdateDate { get; set; }

    public bool Editable { get; set; }

    public long UserId { get; set; }

    [JsonIgnore]
    public virtual ICollection<UserRole> UserRoles { get; set; } = [];
}

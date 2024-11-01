namespace net_moto_bot.Domain.Entities;

public partial class UserRole
{
    public long UserId { get; set; }

    public int RoleId { get; set; }

    public DateTime CreationDate { get; set; }

    public virtual Role Role { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}

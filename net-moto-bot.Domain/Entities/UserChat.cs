namespace net_moto_bot.Domain.Entities;


public partial class UserChat
{
    public int Id { get; set; }

    public long UserId { get; set; }

    public string Code { get; set; } = null!;

    public string Uddi { get; set; } = null!;

    public DateTime CreationDate { get; set; }

    public DateTime UpdateDate { get; set; }

    public string ChatName { get; set; } = null!;

    public string? Icon { get; set; }

    public string? ImageUrl { get; set; }

    public virtual User User { get; set; } = null!;
}

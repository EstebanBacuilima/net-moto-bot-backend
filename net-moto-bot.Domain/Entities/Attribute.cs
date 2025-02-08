
namespace net_moto_bot.Domain.Entities;

public partial class Attribute
{
    public short Id { get; set; }

    public string Code { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    public bool Active { get; set; }
    public virtual ICollection<ProductAttribute> ProductAttributes { get; set; } = [];
}

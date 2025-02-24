
namespace net_moto_bot.Domain.Entities;

public partial class Section
{
    public short Id { get; set; }

    public string Code { get; set; } = string.Empty;
                                     
    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    public bool Active { get; set; }

    public DateTime? EndDate { get; set; }

    public int TotalProduct { get; set; }

    public virtual ICollection<ProductSection> ProductSections { get; set; } = [];
}

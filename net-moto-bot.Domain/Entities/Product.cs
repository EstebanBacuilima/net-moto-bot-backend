
namespace net_moto_bot.Domain.Entities;

public partial class Product
{
    public int Id { get; set; }

    public int CategoryId { get; set; }

    public int BrandId { get; set; }

    public string Code { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public string Sku { get; set; } = string.Empty;

    public string? Description { get; set; }

    public bool Active { get; set; }

    public DateTime CreationDate { get; set; }

    public DateTime UpdateDate { get; set; }

    public virtual Brand? Brand { get; set; }

    public virtual Category? Category { get; set; }

    public virtual ICollection<ProductFile> ProductFiles { get; set; } = [];
}

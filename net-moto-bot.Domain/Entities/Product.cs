namespace net_moto_bot.Domain.Entities;

public partial class Product
{
    public int Id { get; set; }

    public int CategoryId { get; set; }

    public int BrandId { get; set; }

    public string Code { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Sku { get; set; } = null!;

    public string? Description { get; set; }

    public bool Active { get; set; }

    public DateTime CreationDate { get; set; }

    public DateTime UpdateDate { get; set; }

    public virtual Brand Brand { get; set; } = null!;

    public virtual Category Category { get; set; } = null!;

    public virtual ICollection<ProductFile> ProductFiles { get; set; } = new List<ProductFile>();
}

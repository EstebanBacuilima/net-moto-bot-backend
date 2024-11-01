namespace net_moto_bot.Domain.Entities;


public partial class ProductFile
{
    public int Id { get; set; }

    public int ProductId { get; set; }

    public string Code { get; set; } = null!;

    public string FileCode { get; set; } = null!;

    public string Url { get; set; } = null!;

    public bool Active { get; set; }

    public DateTime CreationDate { get; set; }

    public DateTime UpdateDate { get; set; }

    public virtual Product Product { get; set; } = null!;
}

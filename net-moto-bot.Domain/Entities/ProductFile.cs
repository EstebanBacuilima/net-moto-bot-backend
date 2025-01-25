using System.Text.Json.Serialization;

namespace net_moto_bot.Domain.Entities;

public partial class ProductFile
{
    public int Id { get; set; }

    public int ProductId { get; set; }

    public string Code { get; set; } = string.Empty;

    public string FileCode { get; set; } = string.Empty;

    public string Url { get; set; } = string.Empty;

    public bool Active { get; set; }

    public DateTime CreationDate { get; set; }

    public DateTime UpdateDate { get; set; }

    [JsonIgnore]
    public virtual Product? Product { get; set; }
}

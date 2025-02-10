
using System.Text.Json.Serialization;

namespace net_moto_bot.Domain.Entities;

public partial class ProductAttribute
{
    public int ProductId { get; set; }

    public short AttributeId { get; set; }

    public string Value { get; set; } = null!;

    public bool Active { get; set; }

    public virtual Attribute? Attribute { get; set; }

    [JsonIgnore]
    public virtual Product? Product { get; set; } 
}


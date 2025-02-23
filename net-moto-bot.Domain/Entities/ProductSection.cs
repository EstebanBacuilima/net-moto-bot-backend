
using System.Text.Json.Serialization;

namespace net_moto_bot.Domain.Entities;

public partial class ProductSection
{
    public int ProductId { get; set; }

    public short SectionId { get; set; }

    public bool Active { get; set; }

    public virtual Product? Product { get; set; }

    [JsonIgnore] 
    public virtual Section? Section { get; set; } 
}

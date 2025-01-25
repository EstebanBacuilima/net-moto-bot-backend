using System.Text.Json.Serialization;

namespace net_moto_bot.Domain.Entities;

public partial class Person
{
    public int Id { get; set; }

    public string Code { get; set; } = null!;

    public string IdCard { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? Email { get; set; }

    public bool Active { get; set; }

    public DateTime CreationDate { get; set; }

    public DateTime UpadateDate { get; set; }

    [JsonIgnore]
    public virtual ICollection<Customer> Customers { get; set; } = new List<Customer>();

    [JsonIgnore]
    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();

    [JsonIgnore]
    public virtual ICollection<User> Users { get; set; } = new List<User>();
}

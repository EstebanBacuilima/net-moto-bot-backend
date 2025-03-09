using net_moto_bot.Domain.Enums.Custom;
using net_moto_bot.Domain.Exceptions.BadRequest;
using System.Text.Json.Serialization;

namespace net_moto_bot.Domain.Entities;

public partial class User
{
    public long Id { get; set; }

    public int PersonId { get; set; }

    /// <summary>
    /// Unique string code of the user 1 to 128 characters.
    /// </summary>
    public string Code { get; set; } = null!;

    public string? DisplayName { get; set; }

    public string Email { get; set; } = null!;

    public string? Password { get; set; }

    public string? PhotoUrl { get; set; }

    public string? PhoneNumber { get; set; }

    public bool Disabled { get; set; }

    public bool IsManagement { get; set; }

    public string? VerificationCode { get; set; }

    //[JsonIgnore]
    //public virtual ICollection<Customer> Customers { get; set; } = [];

    [JsonIgnore]
    public virtual ICollection<Employee> Employees { get; set; } = [];

    public virtual Person Person { get; set; } = null!;

    [JsonIgnore]
    public virtual ICollection<UserChat> UserChats { get; set; } = [];

    public virtual ICollection<UserRole> UserRoles { get; set; } = [];


    /// <summary>
    /// Validates the User object.
    /// Throws an exception if any validation rule fails.
    /// </summary>
    public void Validate()
    {
        if (string.IsNullOrWhiteSpace(DisplayName))
        {
            throw new BadRequestException(ExceptionEnum.InvalidFirstName);
        }
        if (string.IsNullOrWhiteSpace(Email))
        {
            throw new BadRequestException(ExceptionEnum.InvalidEmail);
        }

        if (!string.IsNullOrEmpty(PhoneNumber) && !PhoneNumber.All(char.IsDigit))
        {
            throw new BadRequestException(ExceptionEnum.InvalidFirstName);
        }
    }
}

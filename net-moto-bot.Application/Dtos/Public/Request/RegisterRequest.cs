namespace net_moto_bot.Application.Dtos.Public.Request;

public class RegisterRequest
{
    public string IdCard { get; set; } = string.Empty;

    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;

    public string? PhotoUrl { get; set; }

    public string? PhoneNumber { get; set; }

    public bool Disabled { get; set; }

    public bool IsManagement { get; set; }
}

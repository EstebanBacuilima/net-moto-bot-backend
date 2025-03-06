namespace net_moto_bot.Application.Dtos.Public.Request;

public class PersonDto
{
    public string IdCard { get; set; } = string.Empty;

    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public string? Email { get; set; }

    public bool Active { get; set; }
}

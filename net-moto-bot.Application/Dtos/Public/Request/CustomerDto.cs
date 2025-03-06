namespace net_moto_bot.Application.Dtos.Public.Request;

public class CustomerDto
{
    public int PersonId { get; set; }

    public string Code { get; set; } = string.Empty;

    public bool Active { get; set; }

    public PersonDto Person { get; set; } = new PersonDto();
}

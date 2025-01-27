namespace net_moto_bot.Application.Dtos.Public.Response;

public class UserChatResponseDto
{
    public int Id { get; set; }

    public string Code { get; set; } = string.Empty;

    public string Uddi { get; set; } = string.Empty;

    public string ChatName { get; set; } = string.Empty;

    public string? Icon { get; set; }

    public string? ImageUrl { get; set; }

}

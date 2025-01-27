
namespace net_moto_bot.Application.Dtos.Public.Response;

public class BotResponseDto
{
    public string Text { get; set; } = string.Empty;

    public DateTime Date { get; set; }

    public short Type { get; set; }
}

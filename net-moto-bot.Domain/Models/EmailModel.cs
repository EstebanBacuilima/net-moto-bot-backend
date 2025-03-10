namespace net_moto_bot.Domain.Models;

public class EmailModel
{
    public string To { get; set; } = string.Empty;
    public string Subject { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
}

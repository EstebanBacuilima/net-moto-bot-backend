
namespace net_moto_bot.Application.Dtos.Public.Response;

public class TokenResponseDto
{
    public string Token { get; set; } = string.Empty;
    public string? UserCode { get; set; }
    public string? DisplayName { get; set; }
    public string? PhotoUrl { get; set; }
    public bool IsManagement { get; set; }
}

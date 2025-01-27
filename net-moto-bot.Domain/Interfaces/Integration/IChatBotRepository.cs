
namespace net_moto_bot.Domain.Interfaces.Integration;

public interface IChatBotRepository
{
    public Task<string> SendUserQueryAsync(string userQuery);
}

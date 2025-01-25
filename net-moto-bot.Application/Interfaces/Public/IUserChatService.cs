using net_moto_bot.Application.Dtos.Public.Request;

namespace net_moto_bot.Application.Interfaces.Public;

public interface IUserChatService
{
    public Task<List<UserChatResponseDto>> GetAllCustomByUserIdAsync(long userId);
}

using net_moto_bot.Domain.Entities;

namespace net_moto_bot.Domain.Interfaces.Public;

public interface IUserChatRepository
{
    public Task<List<UserChat>> FindAllCustomByUserIdAsync(long userId);
}

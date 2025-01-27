
using net_moto_bot.Domain.Entities;

namespace net_moto_bot.Domain.Interfaces.Public;

public interface IUserChatRepository
{
    public Task<List<UserChat>> FindAllCustomByUserIdAsync(long userId);

    public Task<UserChat> SaveAsync(UserChat userChat);

    public Task<UserChat> UpdateAsync(UserChat userChat);

    public UserChat? FindByUserId(long userId);

    public UserChat? FindById(int id);

    public UserChat? FindByCode(string code);

    public UserChat? FindByCodeAndUserId(string code, long userId);
}

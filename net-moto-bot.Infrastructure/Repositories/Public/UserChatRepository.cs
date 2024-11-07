using Microsoft.EntityFrameworkCore;
using net_moto_bot.Domain.Entities;
using net_moto_bot.Domain.Interfaces.Public;
using net_moto_bot.Infrastructure.Connectoins;

namespace net_moto_bot.Infrastructure.Repositories.Public;

public class UserChatRepository(PostgreSQLContext _context) : IUserChatRepository
{
    public Task<List<UserChat>> FindAllCustomByUserIdAsync(long userId)
    {
        return _context.UserChats.AsNoTracking()
            .Where(uc => uc.UserId == userId)
            .Select(uc => new UserChat
            {
                Id = uc.Id,
                Code = uc.Code,
                ChatName = uc.ChatName,
                Uddi = uc.Uddi,
                Icon = uc.Icon,
                ImageUrl = uc.ImageUrl,
            }).ToListAsync();
    }
}

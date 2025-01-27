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

    public async Task<UserChat> SaveAsync(UserChat userChat)
    {
        await _context.UserChats.AddAsync(userChat);

        await _context.SaveChangesAsync();

        return userChat;
    }

    public async Task<UserChat> UpdateAsync(UserChat userChat)
    {
        _context.UserChats.Update(userChat);

        await _context.SaveChangesAsync();

        return userChat;
    }

    public UserChat? FindByUserId(long userId)
    {
        return _context.UserChats.AsNoTracking()
            .Where(uc => uc.UserId == userId).FirstOrDefault();
    }

    public UserChat? FindById(int id)
    {
        return _context.UserChats.AsNoTracking()
            .Where(uc => uc.Id == id).FirstOrDefault();
    }

    public UserChat? FindByCode(string code)
    {
        return _context.UserChats.AsNoTracking()
          .Where(uc => uc.Code.Equals(code)).FirstOrDefault();
    }
    public UserChat? FindByCodeAndUserId(string code, long userId)
    {
        return _context.UserChats.AsNoTracking()
          .Where(uc => uc.Code.Equals(code) && uc.UserId == userId).FirstOrDefault();
    }
}

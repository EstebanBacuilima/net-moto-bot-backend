using net_moto_bot.Application.Dtos.Public.Request;
using net_moto_bot.Application.Dtos.Public.Response;
using net_moto_bot.Domain.Entities;

namespace net_moto_bot.Application.Interfaces.Public;

public interface IUserChatService
{
    public Task<List<UserChatResponseDto>> GetAllCustomByTokenAsync(string token);

    public Task<UserChat> CreateAsync(UserChat userChat, string token);

    public Task<BotResponseDto> CreateUserQueryAsync(UserQueryRequestDto userQueryRequest, string token);

    public Task<UserChat> UpdateAsync(UserChat userChat);

    public UserChat? GetByUserId(long userId);

    public UserChat? GetByCode(string code);
}
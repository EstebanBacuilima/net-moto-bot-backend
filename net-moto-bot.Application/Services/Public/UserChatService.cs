
using AutoMapper;
using net_moto_bot.Application.Dtos.Public.Request;
using net_moto_bot.Application.Interfaces.Public;
using net_moto_bot.Domain.Interfaces.Public;

namespace net_moto_bot.Application.Services.Public;

public class UserChatService(
    IUserChatRepository _respository, IMapper _mapper) : IUserChatService
{
    public async Task<List<UserChatResponseDto>> GetAllCustomByUserIdAsync(long userId)
    {
        return _mapper.Map<List<UserChatResponseDto>>(
            await _respository.FindAllCustomByUserIdAsync(userId)
        );
    }
}

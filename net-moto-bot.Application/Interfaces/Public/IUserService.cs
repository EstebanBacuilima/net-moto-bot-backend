
using net_moto_bot.Application.Dtos.Public;
using net_moto_bot.Domain.Entities;

namespace net_moto_bot.Application.Interfaces.Public;

public interface IUserService
{
    public Task<List<User>> GetAllAsync();

    public Task<string> SignInAsync(LoginRequestDto loginRequestDto);
}

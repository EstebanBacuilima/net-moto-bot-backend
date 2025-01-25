using net_moto_bot.Application.Dtos.Public.Request;
using net_moto_bot.Application.Dtos.Public.Response;
using net_moto_bot.Domain.Entities;

namespace net_moto_bot.Application.Interfaces.Public;

public interface IUserService
{
    public Task<List<User>> GetAllAsync();

    public Task<User?> GetByCodeAsync(string code);

    public Task<User?> GetByIdAsync(long id);

    public Task<TokenResponseDto> SignInAsync(LoginRequestDto loginRequestDto);
}

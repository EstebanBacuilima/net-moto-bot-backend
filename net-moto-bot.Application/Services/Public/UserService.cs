using net_moto_bot.Application.Dtos.Public;
using net_moto_bot.Application.Interfaces.Custom;
using net_moto_bot.Application.Interfaces.Public;
using net_moto_bot.Domain.Entities;
using net_moto_bot.Domain.Enums.Custom;
using net_moto_bot.Domain.Exceptions.Unauthorized;
using net_moto_bot.Domain.Interfaces.Public;

namespace net_moto_bot.Application.Services.Public;

public class UserService(
    IUserRepository _repository, 
    IJWTService _jwtService) : IUserService
{
    public Task<List<User>> GetAllAsync()
    {       
        return _repository.FindAllAsync();
    }

    public Task<User?> GetByCodeAsync(string code) 
    {
        return _repository.FindByCodeAsync(code);
    }

    public Task<User?> GetByIdAsync(long id)
    {
        return _repository.FindByIdAsync(id);
    }

    public async Task<string> SignInAsync(LoginRequestDto loginRequestDto)
    {
        User? user = await _repository.FindByEmailAsync(loginRequestDto.Email.Trim()) ?? throw new BadCredentialException(ExceptionEnum.UserNotFound);

        if (user.Disabled) throw new AccountException(ExceptionEnum.UserDisabled);

        if (!BCrypt.BCrypt.CheckPassword(loginRequestDto.Password, user.Password ?? string.Empty)) throw new BadCredentialException(ExceptionEnum.WrongPassword);

        return _jwtService.GenerateToken(user, 3600);
    }
}
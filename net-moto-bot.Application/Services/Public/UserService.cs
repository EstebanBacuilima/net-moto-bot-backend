using net_moto_bot.Application.Dtos.Public.Request;
using net_moto_bot.Application.Dtos.Public.Response;
using net_moto_bot.Application.Interfaces.Custom;
using net_moto_bot.Application.Interfaces.Public;
using net_moto_bot.Domain.Entities;
using net_moto_bot.Domain.Enums.Custom;
using net_moto_bot.Domain.Exceptions.Unauthorized;
using net_moto_bot.Domain.Interfaces.Public;

namespace net_moto_bot.Application.Services.Public;

public class UserService(
    IUserRepository _repository,
    IPersonRepository _personRepository,
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

    public async Task<TokenResponseDto> ResgisterAsync(User user, bool managment = false)
    {
        if (await _repository.FindByEmailAsync(user.Email.Trim()) != null) throw new BadCredentialException(ExceptionEnum.UsernameAlreadyExist);
        // Validate user and person data.
        user.Validate();
        if (user.Person != null)
        {

        }
        // Encrypt password.
        string salt = BCrypt.BCrypt.GenSalt(12);
        if (user.Password != null) user.Password = BCrypt.BCrypt.HashPassword(user.Password, salt);
        // Save user 
        user = await _repository.AddAsync(user);
        // Return token.
        return new()
        {
            Token = _jwtService.GenerateToken(user, 3600),
            DisplayName = user.DisplayName,
            PhotoUrl = user.PhotoUrl
        };
    }


    public async Task<TokenResponseDto> SignInAsync(LoginRequestDto loginRequestDto)
    {
        User? user = await _repository.FindByEmailAsync(loginRequestDto.Email.Trim()) ?? throw new BadCredentialException(ExceptionEnum.UserNotFound);

        if (user.Disabled) throw new AccountException(ExceptionEnum.UserDisabled);

        if (!BCrypt.BCrypt.CheckPassword(loginRequestDto.Password, user.Password ?? string.Empty)) throw new BadCredentialException(ExceptionEnum.WrongPassword);

        return new()
        {
            Token = _jwtService.GenerateToken(user, 3600),
            DisplayName = user.DisplayName,
            PhotoUrl = user.PhotoUrl
        };
    }


}
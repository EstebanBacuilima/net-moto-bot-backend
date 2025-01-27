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

    public async Task<TokenResponseDto> ResgisterAsync(RegisterRequest request, bool managment = false)
    {
        if (await _repository.FindByEmailAsync(request.Email.Trim()) != null) throw new BadCredentialException(ExceptionEnum.EmailAlreadyExists);
        // Validate user and person data.
        if (string.IsNullOrEmpty(request.IdCard.Trim())) throw new BadCredentialException(ExceptionEnum.IdCardRequired);
        if (await _personRepository.ExistIdCardAsync(request.IdCard)) throw new BadCredentialException(ExceptionEnum.IdCardAlreadyExists);
        if (string.IsNullOrEmpty(request.FirstName.Trim())) throw new BadCredentialException(ExceptionEnum.FirstNameRequired);
        if (string.IsNullOrEmpty(request.LastName.Trim())) throw new BadCredentialException(ExceptionEnum.LastNameRequired);
        if (string.IsNullOrEmpty(request.Email.Trim())) throw new BadCredentialException(ExceptionEnum.EmailRequired);
        if (string.IsNullOrEmpty(request.Password.Trim()) || request.Password.Length < 4) throw new BadCredentialException(ExceptionEnum.WeakPassword);
        // Create person.
        var person = new Person
        {
            IdCard = request.IdCard,
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
        };
        // Create user.
        User user = new()
        {
            DisplayName = $"{request.FirstName} {request.LastName}",
            Email = request.Email,
            Password = request.Password,
            PhotoUrl = request.PhotoUrl,
            PhoneNumber = request.PhoneNumber,
            Disabled = false,
            IsManagement = managment,
            Person = person,
        };

        // Encrypt password.
        string salt = BCrypt.BCrypt.GenSalt(12);
        if (request.Password != null) request.Password = BCrypt.BCrypt.HashPassword(request.Password, salt);
        // Save user
        user.Password = request.Password;
        user = await _repository.AddAsync(user);
        // Return token.
        return new()
        {
            Token = _jwtService.GenerateToken(user, 3600),
            UserCode = user.Code,
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
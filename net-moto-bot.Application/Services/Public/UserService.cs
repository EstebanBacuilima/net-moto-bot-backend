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
        if (await _repository.FindByEmailAsync(request.Email) != null) throw new BadCredentialException(ExceptionEnum.EmailAlreadyExists);
        // Validate user and person data.
        if (string.IsNullOrWhiteSpace(request.IdCard)) throw new BadCredentialException(ExceptionEnum.IdCardRequired);
        if (await _personRepository.ExistIdCardAsync(request.IdCard)) throw new BadCredentialException(ExceptionEnum.IdCardAlreadyExists);
        if (string.IsNullOrWhiteSpace(request.FirstName)) throw new BadCredentialException(ExceptionEnum.FirstNameRequired);
        if (string.IsNullOrWhiteSpace(request.LastName)) throw new BadCredentialException(ExceptionEnum.LastNameRequired);
        if (string.IsNullOrWhiteSpace(request.Email)) throw new BadCredentialException(ExceptionEnum.EmailRequired);
        if (string.IsNullOrWhiteSpace(request.Password) || request.Password.Length < 4) throw new BadCredentialException(ExceptionEnum.WeakPassword);
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
            PhotoUrl = user.PhotoUrl,
            IsManagement = user.IsManagement
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
            PhotoUrl = user.PhotoUrl,
            IsManagement = user.IsManagement
        };
    }

    public async Task<User> UpdateAsync(RegisterRequest request)
    {
        // Find th user.
        var userFinded = await _repository.FindByCodeAsync(request.Code);
        if (userFinded == null) throw new BadCredentialException(ExceptionEnum.UserNotFound);
        Person? person = null;
        // Create person.
        if (userFinded.Person != null)
        {
            person = new()
            {
                Code = userFinded.Person.Code,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
            };
        }
        // Create user.
        User user = new()
        {
            Code = request.Code,
            DisplayName = $"{request.FirstName} {request.LastName}",
            PhotoUrl = request.PhotoUrl,
            PhoneNumber = request.PhoneNumber,
            Person = person ?? new(),
        };
        if (user.Person != null && user.Person.Id != 0) person = await _personRepository.UpdateAsync(user.Person);
        user = await _repository.UpdateAsync(user);
        user.Person = person ?? new();
        return user;
    }
}
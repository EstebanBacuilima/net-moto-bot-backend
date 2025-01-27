
using AutoMapper;
using net_moto_bot.Application.Dtos.Public.Request;
using net_moto_bot.Application.Dtos.Public.Response;
using net_moto_bot.Application.Interfaces.Custom;
using net_moto_bot.Application.Interfaces.Public;
using net_moto_bot.Application.Services.Custom;
using net_moto_bot.Domain.Entities;
using net_moto_bot.Domain.Exceptions.BadRequest;
using net_moto_bot.Domain.Interfaces.Integration;
using net_moto_bot.Domain.Interfaces.Public;
using System.Text.Json;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace net_moto_bot.Application.Services.Public;

public class UserChatService(
    IUserChatRepository _respository,
    IJWTService _JWTService,
    IMapper _mapper,
    IUserRepository _userRepository,
    IChatBotRepository _chatBotRepository) : IUserChatService
{
    public async Task<List<UserChatResponseDto>> GetAllCustomByTokenAsync(string token)
    {
        long userId = _JWTService.GetUserId(token);

        if (!await _userRepository.ExistsByIdAsync(userId)) throw new BadRequestException(Domain.Enums.Custom.ExceptionEnum.UserNotFound);

        return _mapper.Map<List<UserChatResponseDto>>(
            await _respository.FindAllCustomByUserIdAsync(userId)
        );
    }

    public async Task<BotResponseDto> CreateUserQueryAsync(UserQueryRequestDto userQueryRequest, string token)
    {
        try
        {
            Dictionary<string, object?> keyValuePairs = [];
            keyValuePairs.Add("", token);

            long userId = _JWTService.GetUserId(token);

            UserChat userChat = _respository.FindByCodeAndUserId(userQueryRequest.ChatCode, userId)
                ?? throw new BadRequestException(Domain.Enums.Custom.ExceptionEnum.UserNotFound); ;

            string response = await _chatBotRepository.SendUserQueryAsync(userQueryRequest.UserQuery);

            return new()
            {
                Text = ExtractDataFromJson(response),
                Date = DateTime.UtcNow,
                Type = 2
            };

        }
        catch
        {
            string response = await _chatBotRepository.SendUserQueryAsync(userQueryRequest.UserQuery);

            return new()
            {
                Text = ExtractDataFromJson(response),
                Date = DateTime.UtcNow,
                Type = 2

            };
        }
    }

    private static string ExtractDataFromJson(string jsonResponse)
    {
        using var document = JsonDocument.Parse(jsonResponse);
        var root = document.RootElement;

        if (root.TryGetProperty("data", out JsonElement dataElement))
        {
            return dataElement.GetString();
        }

        return jsonResponse;
    }

    public async Task<UserChat> CreateAsync(UserChat userChat, string token)
    {
        long userId = _JWTService.GetUserId(token);

        if (!await _userRepository.ExistsByIdAsync(userId)) throw new BadRequestException(Domain.Enums.Custom.ExceptionEnum.UserNotFound);

        userChat.UserId = userId;

        return await _respository.SaveAsync(userChat);
    }

    public Task<UserChat> UpdateAsync(UserChat userChat)
    {
        UserChat finded = _respository.FindByCode(userChat.Code)
            ?? throw new BadRequestException(Domain.Enums.Custom.ExceptionEnum.UserNotFound);

        finded.ChatName = userChat.ChatName;
        finded.Icon = userChat.Icon;
        finded.ImageUrl = userChat.ImageUrl;

        return _respository.UpdateAsync(finded);
    }

    public UserChat? GetByUserId(long userId)
    {
        return _respository.FindByUserId(userId);
    }

    public UserChat? GetByCode(string code)
    {
        return _respository.FindByCode(code);
    }
}


using AutoMapper;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using net_moto_bot.Application.Dtos.Public.Request;
using net_moto_bot.Application.Dtos.Public.Response;
using net_moto_bot.Application.Interfaces.Custom;
using net_moto_bot.Application.Interfaces.Mongo;
using net_moto_bot.Application.Interfaces.Public;
using net_moto_bot.Domain.Entities;
using net_moto_bot.Domain.Enums.Custom;
using net_moto_bot.Domain.Enums.Public;
using net_moto_bot.Domain.Exceptions.BadRequest;
using net_moto_bot.Domain.Interfaces.Integration;
using net_moto_bot.Domain.Interfaces.Public;
using System.Text.Json;

namespace net_moto_bot.Application.Services.Public;

public class UserChatService(
    IUserChatRepository _respository,
    IJWTService _JWTService,
    IMapper _mapper,
    IUserRepository _userRepository,
    IChatBotRepository _chatBotRepository,
    IMongoService _mongoService) : IUserChatService
{
    public async Task<List<UserChatResponseDto>> GetAllCustomByTokenAsync(string token)
    {
        long userId = _JWTService.GetUserId(token);

        if (!await _userRepository.ExistsByIdAsync(userId)) throw new BadRequestException(Domain.Enums.Custom.ExceptionEnum.UserNotFound);

        return _mapper.Map<List<UserChatResponseDto>>(
            await _respository.FindAllCustomByUserIdAsync(userId)
        );
    }

    public async Task<List<Dictionary<string, object?>>> GetAllMessagesByChatCodeAsync(string chatCode)
    {
        UserChat userChat = _respository.FindByCode(chatCode)
            ?? throw new BadRequestException(ExceptionEnum.UserNotFound);

        if (userChat.Uddi.Length != 20) return [];

        var filter = Builders<BsonDocument>.Filter.Eq("_id", ObjectId.Parse(userChat.Uddi));
        var result = await _mongoService.GetDataAsync(filter);

        return result.Contains("messages") && result["messages"] is BsonArray bsonMessages
            ? bsonMessages.Select(b => BsonSerializer.Deserialize<Dictionary<string, object?>>(b.AsBsonDocument)).ToList()
            : [];
    }

    public async Task<BotResponseDto> GetQueryAsync(UserQueryRequestDto userQueryRequest)
    {
        string response = await _chatBotRepository.SendUserQueryAsync(userQueryRequest.UserQuery);

        //var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(response);

        return new()
        {
            Text = response,
            //Text = Convert.ToBase64String(plainTextBytes),
            Date = DateTime.UtcNow,
            Type = (short)ChatTypeEnum.Bot
        };
    }

    public async Task<BotResponseDto> CreateUserQueryAsync(UserQueryRequestDto userQueryRequest, string token)
    {
        try
        {
            List<Dictionary<string, object?>> dictionaries = [];

            Dictionary<string, object?> dictionaryRequest = [];
            dictionaryRequest.Add("text", userQueryRequest.UserQuery);
            dictionaryRequest.Add("date", DateTime.UtcNow);
            dictionaryRequest.Add("type", (short)ChatTypeEnum.User);
            dictionaries.Add(dictionaryRequest);

            long userId = _JWTService.GetUserId(token);

            UserChat userChat = _respository.FindByCodeAndUserId(userQueryRequest.ChatCode, userId)
                ?? throw new BadRequestException(Domain.Enums.Custom.ExceptionEnum.UserNotFound); ;

            string response = await _chatBotRepository.SendUserQueryAsync(userQueryRequest.UserQuery);

            string data = ExtractDataFromJson(response);

            Dictionary<string, object?> dictionaryResponse = [];
            dictionaryResponse.Add("text", data);
            dictionaryResponse.Add("date", DateTime.UtcNow);
            dictionaryResponse.Add("type", (short)ChatTypeEnum.Bot);
            dictionaries.Add(dictionaryResponse);

            (Dictionary<string, object?> responseUddi, bool exixts) = await _mongoService.SaveOrUpdateAsync(userChat.Uddi, dictionaries);

            if (responseUddi.TryGetValue("_id", out object? value) && value != null)
            {
                string documentId = value.ToString()!;

                userChat.Uddi = documentId;

                if (!exixts) await _respository.UpdateAsync(userChat);
            }

            return new()
            {
                Text = data,
                Date = DateTime.UtcNow,
                Type = (short)ChatTypeEnum.Bot
            };

        }
        catch
        {
            //string response = await _chatBotRepository.SendUserQueryAsync(userQueryRequest.UserQuery);

            return new()
            {
                Text = "Lo sentimos, no pudimos procesar su solicitud en este momento, inténtelo nuevamente más tarde.",
                Date = DateTime.UtcNow,
                Type = (short)ChatTypeEnum.Bot

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

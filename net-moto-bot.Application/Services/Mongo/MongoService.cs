
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Driver;
using net_moto_bot.Application.Interfaces.Mongo;
using net_moto_bot.Domain.Enums.Custom;
using net_moto_bot.Domain.Exceptions.BadRequest;
using net_moto_bot.Domain.Interfaces.Mongo;
using System.Text.Json;

namespace net_moto_bot.Application.Services.Mongo;

public class MongoService(
    IMongoRepository _repository) : IMongoService
{
    private readonly JsonSerializerOptions _serializerOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        PropertyNameCaseInsensitive = true,
    };

    public async Task<Dictionary<string, object?>> AddOrUpdate(string _id, Dictionary<string, object?> document)
    {
        string collectionName = "chats";

        string json = JsonSerializer.Serialize(document.ToDictionary(
            kvp => char.ToLowerInvariant(kvp.Key[0]) + kvp.Key[1..],
            kvp => kvp.Value
        ), _serializerOptions);

        BsonDocument bsonDocument = BsonDocument.Parse(json);

        // Check if exist document
        bool existDocument = await _repository.ExistsByCodeAndIdAsync(
            collectionName: collectionName, string.Empty
        );
        if (existDocument)
        {
            FilterDefinition<BsonDocument> filter = Builders<BsonDocument>.Filter.Eq("_id", _id);
            await _repository.UpdateAsync(collectionName, filter, bsonDocument);
        }
        else
        {
            await _repository.SaveAsync(collectionName, bsonDocument);
        }
        return bsonDocument.ToDictionary();
    }

}

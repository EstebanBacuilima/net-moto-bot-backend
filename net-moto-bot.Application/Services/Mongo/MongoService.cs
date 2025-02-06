
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using net_moto_bot.Application.Interfaces.Mongo;
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

    public async Task<(Dictionary<string, object?>, bool)> SaveOrUpdateAsync(string _id, List<Dictionary<string, object?>> documents)
    {
        string collectionName = "chats";

        Dictionary<string, object?> dictionary = [];
        bool existDocument = await _repository.ExistsByCodeAndIdAsync(collectionName, _id);

        if (existDocument)
        {
            FilterDefinition<BsonDocument> filter = Builders<BsonDocument>.Filter.Eq("_id", ObjectId.Parse(_id));
            BsonDocument existingDocument = await _repository.FindDataAsync(collectionName, filter);

            List<Dictionary<string, object?>> existingMessages = existingDocument["messages"]
                .AsBsonArray
                .Select(bsonValue => BsonSerializer.Deserialize<Dictionary<string, object?>>(bsonValue.AsBsonDocument))
                .ToList();

            existingMessages.AddRange(documents);

            dictionary["messages"] = existingMessages;

            string json = JsonSerializer.Serialize(dictionary, _serializerOptions);
            BsonDocument updatedDocument = BsonDocument.Parse(json);

            await _repository.UpdateAsync(collectionName, filter, updatedDocument);

            return (updatedDocument.ToDictionary(), existDocument);
        }
        else
        {
            dictionary["messages"] = documents;

            string json = JsonSerializer.Serialize(dictionary, _serializerOptions);
            BsonDocument newDocument = BsonDocument.Parse(json);

            await _repository.SaveAsync(collectionName, newDocument);
            return (newDocument.ToDictionary(), existDocument) ;
        }

        //return (dictionary, existDocument);
    }

    public Task<BsonDocument> GetDataAsync(FilterDefinition<BsonDocument> filter) 
    {
        return  _repository.FindDataAsync("chats", filter);
    }
}

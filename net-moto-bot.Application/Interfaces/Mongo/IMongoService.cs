
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections;

namespace net_moto_bot.Application.Interfaces.Mongo;

public interface IMongoService
{
    public Task<(Dictionary<string, object?>, bool)> SaveOrUpdateAsync(string _id, List<Dictionary<string, object?>> documents);

    public Task<BsonDocument> GetDataAsync(FilterDefinition<BsonDocument> filter);
}

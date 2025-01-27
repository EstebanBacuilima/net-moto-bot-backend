using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace net_moto_bot.Infrastructure.Connections.Mongo;

public class MongoDBContext(IMongoClient client, IOptions<MongoDBSetting> setting)
{
    private readonly IMongoDatabase _database = client.GetDatabase(setting.Value.DatabaseName);

    public IMongoCollection<BsonDocument> GetCollection(string collectionName)
    {
        return _database.GetCollection<BsonDocument>(collectionName);
    }
}
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using MongoDB.Driver;

namespace net_moto_bot.Domain.Interfaces.Mongo;

public interface IMongoRepository
{
    public Task<bool> ExistsByCodeAndIdAsync(string collectionName, string _id);

    public Task<BsonDocument> FindDataAsync(string collectionName, FilterDefinition<BsonDocument> filter);

    public Task<BsonDocument> SaveAsync(string collectionName, BsonDocument document);

    public Task<UpdateResult> UpdateAsync(string collectionName, FilterDefinition<BsonDocument> filter, BsonDocument update);
}

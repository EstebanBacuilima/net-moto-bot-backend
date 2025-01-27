
using MongoDB.Bson;
using MongoDB.Driver;
using net_moto_bot.Domain.Interfaces.Mongo;
using net_moto_bot.Infrastructure.Connections.Mongo;

namespace net_moto_bot.Infrastructure.Repositories.Mongo;

public class MongoRepository(MongoDBContext _context) : IMongoRepository
{
    public async Task<bool> ExistsByCodeAndIdAsync(string collectionName, string _id) 
    {
        FilterDefinition<BsonDocument> filter = Builders<BsonDocument>.Filter.Eq("_id", _id);

        long count = await _context.GetCollection(collectionName).CountDocumentsAsync(filter);
        return count > 0;
    }

    public async Task<BsonDocument> FindDataAsync(string collectionName, FilterDefinition<BsonDocument> filter) 
    {
        return await _context.GetCollection(collectionName).Find(filter).FirstOrDefaultAsync();
    }

    public async Task<BsonDocument> SaveAsync(string collectionName, BsonDocument document)
    {
        await _context.GetCollection(collectionName).InsertOneAsync(document);
        return document;
    }

    public Task<UpdateResult> UpdateAsync(string collectionName, FilterDefinition<BsonDocument> filter, BsonDocument update)
    {
        return _context.GetCollection(collectionName).UpdateOneAsync(filter, new BsonDocument("$set", new BsonDocument(update)));
    }
}

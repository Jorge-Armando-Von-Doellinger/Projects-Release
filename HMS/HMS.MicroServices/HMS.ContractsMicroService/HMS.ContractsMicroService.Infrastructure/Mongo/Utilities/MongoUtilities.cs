using HMS.ContractsMicroService.Core.Entity;
using HMS.ContractsMicroService.Infrastructure.Messages;
using MongoDB.Bson;
using MongoDB.Driver;

namespace HMS.ContractsMicroService.Infrastructure.Mongo.Utilities
{
    internal static class MongoUtilities
    {
        internal static ObjectId GetObjectId(string entityId)
        {
            var parseSuccess = ObjectId.TryParse(entityId, out var ID);
            if (!parseSuccess) throw new InvalidOperationException(MessageRecords.InvalidIdentifier);
            return ID;
        }

        internal static FilterDefinition<TEntity> GetFilterID<TEntity>(string entityId)
        {
            return Builders<TEntity>
                .Filter
                .Eq("_id", GetObjectId(entityId));
        }
    }
}

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

        internal static FilterDefinition<T> GetFilterID<T>(string entityId)
        {
            return Builders<T>
                .Filter
                .Eq("_id", entityId);
        }

        internal static FilterDefinition<WorkHours> WorkHoursFilterID(string entityId)
        {
            return Builders<WorkHours>
                .Filter
                .Eq(x => x.ID, entityId);
        }
        internal static FilterDefinition<EmployeeContract> EmployeeContractFilterID(string entityId)
        {
            return Builders<EmployeeContract>
                .Filter
                .Eq(x => x.ID, entityId);
        }
    }
}

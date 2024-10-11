using HMS.ContractsMicroService.Core.Entity;
using HMS.ContractsMicroService.Core.Entity.Base;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace HMS.ContractsMicroService.Infrastructure.Context
{
    public sealed class MongoContext
    {
        private IMongoClient _client;
        public MongoContext(IMongoClient client)
        {
            _client = client;
            RegisterEntityId();
        }

        private void RegisterEntityId()
        {
            if (BsonClassMap.IsClassMapRegistered(typeof(EmployeeContract)) || BsonClassMap.IsClassMapRegistered(typeof(EntityBaseWithId)))
                return;
            BsonClassMap.RegisterClassMap<EmployeeContract>(cm =>
            {
                cm.AutoMap();
                cm.SetIgnoreExtraElements(true);
            });
            BsonClassMap.RegisterClassMap<EntityBaseWithId>(cm =>
            {
                cm.AutoMap();
                cm.SetIgnoreExtraElements(true);
                cm.MapIdProperty(x => x.ID);
            });
        }

        
        internal IMongoClient GetMongoClient()
        {
            return _client;
        }

        internal IMongoCollection<EmployeeContract> GetEmployeeContractsCollection()
        {
            var name = "EmployeeContracts";
            return _client.GetDatabase(name).GetCollection<EmployeeContract>(name);
        }

        internal IMongoCollection<WorkHours> GetWorkHoursCollection()
        {
            var name = "WorkHours";
            return _client.GetDatabase(name).GetCollection<WorkHours>(name);
        }

        internal IMongoCollection<object> GetGeneralContractCollection()
        {
            var name = "GeneralContracts";
            return _client.GetDatabase(name).GetCollection<object>(name);
        }
    }
}

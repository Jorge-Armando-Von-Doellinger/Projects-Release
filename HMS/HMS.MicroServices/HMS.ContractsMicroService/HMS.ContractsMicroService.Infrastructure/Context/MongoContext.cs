﻿using HMS.ContractsMicroService.Core.Entity;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace HMS.ContractsMicroService.Infrastructure.Context
{
    public sealed class MongoContext
    {
        private IMongoClient _client;
        public MongoContext()
        {
            SetMongoClient();
            RegisterEntityId();
        }

        private void RegisterEntityId()
        {
            BsonClassMap.RegisterClassMap<EmployeeContract>(cm =>
            {
                cm.AutoMap();
                cm.MapIdProperty(x => x.ID);
            });
        }

        private void SetMongoClient()
        {
            _client = new MongoClient("mongodb://localhost:27017");
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

using HMS.Payments.Core.Entity;
using HMS.Payments.Core.Entity.Base;
using HMS.Payments.Infrastructure.Settings.Implementations;
using HMS.Payments.Infrastructure.Settings.Interfaces;
using Microsoft.Extensions.Options;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace HMS.Payments.Infrastructure.Connect
{
    public sealed class MongoContext
    {
        private readonly IMongoClient _client;
        private readonly IMongoDatabase _database;
        private readonly IDatabaseSettings _settings;
        public MongoContext(IMongoClient client, IOptionsMonitor<DatabaseSettings> settings)
        {
            _client = client;
            _settings = settings.CurrentValue;
            _database = _client.GetDatabase(_settings.Name);
            MapId();
        }

        void MapId()
        {
            if (BsonClassMap.IsClassMapRegistered(typeof(EntityBase))) return;
            BsonClassMap.RegisterClassMap<EntityBase>(cm =>
            {
                cm.AutoMap();
                cm.SetIgnoreExtraElements(true);
                cm.MapIdProperty(p => p.ID);
            });
        }

        internal IMongoCollection<PaymentEmployee> PaymentEmployee => _database.GetCollection<PaymentEmployee>(_settings.PaymentEmployeeCollection);
        internal IMongoCollection<Payment> Payment => _database.GetCollection<Payment>(_settings.PaymentCollection);    
    }
}

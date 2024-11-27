using HMS.Payments.Core.Entity;
using HMS.Payments.Core.Entity.Base;
using HMS.Payments.Infrastructure.Settings.Implementations;
using HMS.Payments.Infrastructure.Settings.Interfaces;
using HMS.Payments.Infratructure.Services;
using Microsoft.Extensions.Options;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System.Linq;

namespace HMS.Payments.Infrastructure.Context
{
    public sealed class MongoContext
    {
        private readonly TransactionService _transaction;
        private readonly IMongoDatabase _database;
        private readonly IDatabaseSettings _settings;
        public MongoContext(IMongoClient client, TransactionService transaction, IOptionsMonitor<DatabaseSettings> settings)
        {
            _transaction = transaction;
            _settings = settings.CurrentValue;
            _database = client.GetDatabase(_settings.Name);
            MapId();
        }

        private void MapId()
        {
            if (BsonClassMap.IsClassMapRegistered(typeof(EntityBase))) return;
            BsonClassMap.RegisterClassMap<EntityBase>(cm =>
            {   
                cm.AutoMap(); // Mapeia automaticamente todas as propriedades
                cm.SetIgnoreExtraElements(true); // Ignora elementos extras na inserção
                cm.MapIdProperty(c => c.ID);

            });
        }
        internal IMongoCollection<PaymentEmployee> PaymentEmployee => _database.GetCollection<PaymentEmployee>(_settings.PaymentEmployeeCollection);
        internal IMongoCollection<Payment> Payment => _database.GetCollection<Payment>(_settings.PaymentCollection);
    }
}

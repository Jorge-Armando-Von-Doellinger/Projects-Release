using HMS.Payments.Core.Entity;
using HMS.Payments.Core.Entity.Base;
using HMS.Payments.Infrastructure.Generator;
using HMS.Payments.Infrastructure.Serializer;
using HMS.Payments.Infrastructure.Settings.Implementations;
using HMS.Payments.Infrastructure.Settings.Interfaces;
using HMS.Payments.Infratructure.Services;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;

namespace HMS.Payments.Infrastructure.Connect
{
    public sealed class MongoContext
    {
        private readonly IMongoClient _client;
        private readonly TransactionService _transaction;
        private readonly IMongoDatabase _database;
        private readonly IDatabaseSettings _settings;
        private List<WriteModel<Payment>> _paymentOperations = new();
        private List<WriteModel<PaymentEmployee>> _paymentEmployeeOperations = new();
        public MongoContext(IMongoClient client, TransactionService transaction, IOptionsMonitor<DatabaseSettings> settings)
        {
            _client = client;
            _transaction = transaction;
            _settings = settings.CurrentValue;
            _database = _client.GetDatabase(_settings.Name);
            MapId();
        }

        private void MapId()
        {
            if (BsonClassMap.IsClassMapRegistered(typeof(EntityBase))) return;
            BsonClassMap.RegisterClassMap<EntityBase>(cm =>
            {
                cm.AutoMap(); // Mapeia automaticamente todas as propriedades
                cm.SetIgnoreExtraElements(true); // Ignora elementos extras na inserção
                cm.MapIdProperty(c => c.ID)
                    .SetIdGenerator(new StringIdGenerator())
                    .SetSerializer(new ObjectIdStringSerializer());
            });
        }
        internal async Task AddOperation(WriteModel<Payment> operation)
        {
            _paymentOperations.Add(operation);
            await ExecuteOperationsMassive(_paymentOperations, _paymentEmployeeOperations);
        }
        internal async Task AddOperation(WriteModel<PaymentEmployee> operation)
        {
            _paymentEmployeeOperations.Add(operation);
            if (_paymentOperations.Count < 50 && _paymentEmployeeOperations.Count < 50) return;
            await ExecuteOperationsMassive(_paymentOperations, _paymentEmployeeOperations);
        }
        internal async Task ExecuteOperationsMassive(List<WriteModel<Payment>> paymentOperations, List<WriteModel<PaymentEmployee>> paymentEmployeeOperations)
        {
            await _transaction.Execute(async (session) =>
            {
                await Payment.BulkWriteAsync(session, paymentOperations);
                await PaymentEmployee.BulkWriteAsync(session, paymentEmployeeOperations);
            });
            _paymentOperations.RemoveAll(x => paymentOperations.Contains(x));
            _paymentEmployeeOperations.RemoveAll(x => paymentEmployeeOperations.Contains(x));
        }
        internal IMongoCollection<PaymentEmployee> PaymentEmployee => _database.GetCollection<PaymentEmployee>(_settings.PaymentEmployeeCollection);
        internal IMongoCollection<Payment> Payment => _database.GetCollection<Payment>(_settings.PaymentCollection);    
    }
}

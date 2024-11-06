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
                cm.MapIdProperty(c => c.ID);

            });
        }
        internal async Task AddOperation(WriteModel<Payment> operation)
        {
            if (_paymentOperations.Contains(operation)) throw new Exception("Batata");
            _paymentOperations.Add(operation);
            if (_paymentOperations.Count < 50) return;
            else await ExecuteOperationsMassive(_paymentOperations, _paymentEmployeeOperations);
        }
        internal async Task AddOperation(WriteModel<PaymentEmployee> operation)
        {
            if (_paymentEmployeeOperations.Contains(operation)) throw new Exception("Batata");
            _paymentEmployeeOperations.Add(operation);
            if (_paymentOperations.Count < 50) return;
            else await ExecuteOperationsMassive(_paymentOperations, _paymentEmployeeOperations);
        }
        internal async Task ExecuteOperationsMassive(List<WriteModel<Payment>> paymentOperations, List<WriteModel<PaymentEmployee>> paymentEmployeeOperations)
        {
            //await _transaction.Execute(async (session) =>
            //{
                if (paymentOperations.Any())
                {
                    await Payment.BulkWriteAsync(paymentOperations);
                    _paymentOperations.Clear();
                }
                if (paymentEmployeeOperations.Any())
                {

                    await PaymentEmployee.BulkWriteAsync(paymentEmployeeOperations);
                _paymentEmployeeOperations.Clear();
                }

            //});
        }
        internal IMongoCollection<PaymentEmployee> PaymentEmployee => _database.GetCollection<PaymentEmployee>(_settings.PaymentEmployeeCollection);
        internal IMongoCollection<Payment> Payment => _database.GetCollection<Payment>(_settings.PaymentCollection);
    }
}

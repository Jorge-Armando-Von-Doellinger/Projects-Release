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
            _ = LoopTest();
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
        internal void AddOperation(WriteModel<Payment> operation)
        {
            if (_paymentOperations.Contains(operation)) throw new Exception("Batata");
            _paymentOperations.Add(operation);
        }
        internal void AddOperation(WriteModel<PaymentEmployee> operation)
        {
            if (_paymentEmployeeOperations.Contains(operation)) throw new Exception("Batata");
            _paymentEmployeeOperations.Add(operation);
        }

        private async Task LoopTest()
        {
            while (true)
            {
                await Task.Delay(15000);
                List<WriteModel<Payment>> paymentCopy;
                List<WriteModel<PaymentEmployee>> paymentEmployeeCopy;
                lock (_paymentEmployeeOperations)
                {
                    paymentEmployeeCopy = new(_paymentEmployeeOperations);
                    lock (_paymentOperations)
                    {
                        paymentCopy = new(_paymentOperations);
                    }
                    _paymentEmployeeOperations.Clear();
                    _paymentOperations.Clear();
                }
                await ExecuteOperationsMassive(paymentCopy, paymentEmployeeCopy);
            }
        }
        internal async Task ExecuteOperationsMassive(List<WriteModel<Payment>> paymentOperations, List<WriteModel<PaymentEmployee>> paymentEmployeeOperations)
        {
            if (paymentOperations.Count > 0)
            {
                await Payment.BulkWriteAsync(paymentOperations);
            }
            if (paymentEmployeeOperations.Count > 0)
            {
                await PaymentEmployee.BulkWriteAsync(paymentEmployeeOperations);
            }
        }   
        internal IMongoCollection<PaymentEmployee> PaymentEmployee => _database.GetCollection<PaymentEmployee>(_settings.PaymentEmployeeCollection);
        internal IMongoCollection<Payment> Payment => _database.GetCollection<Payment>(_settings.PaymentCollection);
    }
}

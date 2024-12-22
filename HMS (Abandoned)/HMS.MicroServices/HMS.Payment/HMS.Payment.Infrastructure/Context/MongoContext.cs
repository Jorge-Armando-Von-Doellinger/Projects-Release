using HMS.Payments.Core.Entity;
using HMS.Payments.Core.Entity.Base;
using HMS.Payments.Infrastructure.Services;
using HMS.Payments.Infrastructure.Settings.Implementations;
using HMS.Payments.Infrastructure.Settings.Interfaces;
using Microsoft.Extensions.Options;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace HMS.Payments.Infrastructure.Context;

public sealed class MongoContext
{
    private readonly IMongoDatabase _database;
    private readonly IDatabaseSettings _settings;
    private readonly TransactionService _transaction;

    public MongoContext(IMongoClient client, TransactionService transaction,
        IOptionsMonitor<DatabaseSettings> settings)
    {
        _transaction = transaction;
        _settings = settings.CurrentValue;
        _database = client.GetDatabase(_settings.Name);
        MapId();
    }

    internal IMongoCollection<PaymentEmployee> PaymentEmployee =>
        _database.GetCollection<PaymentEmployee>(_settings.PaymentEmployeeCollection);

    internal IMongoCollection<Payment> Payment => _database.GetCollection<Payment>(_settings.PaymentCollection);
    internal IMongoCollection<Refund> Refunds => _database.GetCollection<Refund>(_settings.RefundCollection);

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
}
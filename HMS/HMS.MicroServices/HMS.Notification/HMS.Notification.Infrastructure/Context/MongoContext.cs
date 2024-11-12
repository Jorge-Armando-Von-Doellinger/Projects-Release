using HMS.Notification.Core.Entity;
using HMS.Notification.Infrastructure.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using ZstdSharp.Unsafe;

namespace HMS.Notification.Infrastructure.Context;

public sealed class MongoContext
{
    private readonly IOptionsMonitor<DatabaseSettings> _settings;
    private readonly IMongoClient _mongoClient;
    private List<WriteModel<NotificationEntity>> _operations = new ();
    public MongoContext(IOptionsMonitor<DatabaseSettings> settings)
    {
        _settings = settings;
        _mongoClient = new MongoClient(settings.CurrentValue.ConnectionString);
        MapPropertyId();
        ExecuteOperationsLoop(); // Depois mexer nisso
    }
    private void MapPropertyId()
    {
        if(BsonClassMap.IsClassMapRegistered(typeof(NotificationEntity))) return;
        BsonClassMap.RegisterClassMap<NotificationEntity>(mp =>
        {
            mp.AutoMap();
            mp.MapIdProperty(x => x.Id);
        });
    }
    private IMongoDatabase GetDatabase() => _mongoClient.GetDatabase(_settings.CurrentValue.DatabaseName);
    internal IMongoCollection<NotificationEntity> GetNotificationCollection() => GetDatabase().GetCollection<NotificationEntity>(_settings.CurrentValue.NotificationCollection);
    
    internal void AddOperation(WriteModel<NotificationEntity> writeModel)
    {
        _operations.Add(writeModel);
    }
    private async Task ExecuteOperationsLoop()
    {
        while (true)
        {
            await Task.Delay(15000);
            await ExecuteOperationsLoop();
        }
    }
    
    private async Task ExecuteOperationsAsync()
    {
        if(_operations.Count <= 0) return;
        List<WriteModel<NotificationEntity>> operationsCopy; 
        lock (_operations)
        {
            operationsCopy = new (_operations);
            _operations.Clear();
        }
        await GetNotificationCollection().BulkWriteAsync(operationsCopy);
    }
}
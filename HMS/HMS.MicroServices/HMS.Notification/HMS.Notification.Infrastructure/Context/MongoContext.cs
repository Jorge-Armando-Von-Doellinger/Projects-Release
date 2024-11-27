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
    private readonly MongoClient _mongoClient;
    private List<WriteModel<NotificationBase>> _operations = new ();
    public MongoContext(IOptionsMonitor<DatabaseSettings> settings)
    {
        _settings = settings;
        _mongoClient = new MongoClient(settings.CurrentValue.ConnectionString);
        MapPropertyId();
    }
    private void MapPropertyId()
    {
        if(BsonClassMap.IsClassMapRegistered(typeof(NotificationBase))) return;
        BsonClassMap.RegisterClassMap<NotificationBase>(mp =>
        {
            mp.AutoMap();
            mp.MapIdProperty(x => x.Id);
        });
    }
    private IMongoDatabase GetDatabase() => _mongoClient.GetDatabase(_settings.CurrentValue.DatabaseName);
    internal IMongoCollection<NotificationBase> GetNotificationCollection() => GetDatabase().GetCollection<NotificationBase>(_settings.CurrentValue.NotificationCollection);
    
}
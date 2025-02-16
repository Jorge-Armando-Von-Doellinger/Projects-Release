using Microsoft.Extensions.Options;
using MongoDB.Driver;
using OmniSphere.Payments.Infrastructure.Configs;

namespace OmniSphere.Payments.Infrastructure.Factory;

public class MongoConnectionFactory
{
    private readonly IOptionsMonitor<DatabaseConfigs> _configsMonitor;

    public MongoConnectionFactory(IOptionsMonitor<DatabaseConfigs> configsMonitor)
    {
        _configsMonitor = configsMonitor;
    }
    internal IMongoClient GetMongoClient() => new MongoClient(_configsMonitor.CurrentValue.ConnectionString);
    
}
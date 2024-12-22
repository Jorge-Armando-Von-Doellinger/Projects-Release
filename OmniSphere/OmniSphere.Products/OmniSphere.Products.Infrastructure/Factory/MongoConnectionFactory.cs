using Microsoft.Extensions.Options;
using MongoDB.Driver;
using OmniSphere.Products.Infrastructure.Configs;

namespace OmniSphere.Products.Infrastructure.Factory;

public class MongoConnectionFactory
{
    private readonly IOptionsMonitor<DatabaseConfiguration> _settingsMonitor;

    public MongoConnectionFactory(IOptionsMonitor<DatabaseConfiguration> settingsMonitor)
    {
        _settingsMonitor = settingsMonitor;
    }

    internal IMongoClient GetMongoClient()
    {
        var config = _settingsMonitor.CurrentValue;
        Console.WriteLine(config.ConnectionString);
        var client = new MongoClient(config.ConnectionString);
        return client;
    }
}
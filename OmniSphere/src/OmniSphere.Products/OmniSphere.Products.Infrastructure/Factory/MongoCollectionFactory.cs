using Microsoft.Extensions.Options;
using MongoDB.Driver;
using OmniSphere.Products.Core.Entity;
using OmniSphere.Products.Infrastructure.Configs;
using OmniSphere.Products.Infrastructure.Configurator;

namespace OmniSphere.Products.Infrastructure.Factory;

public class MongoCollectionFactory
{
    private readonly IOptionsMonitor<DatabaseConfiguration> _configurationMonitor;
    private readonly MongoConnectionFactory _mongoConnectionFactory;
    private readonly ProductCollectionConfigurator _configurator;

    public MongoCollectionFactory(IOptionsMonitor<DatabaseConfiguration> configurationMonitor,
        MongoConnectionFactory mongoConnectionFactory,
        ProductCollectionConfigurator configurator)
    {
        _configurationMonitor = configurationMonitor;
        _mongoConnectionFactory = mongoConnectionFactory;
        _configurator = configurator;
    }

    internal IMongoCollection<Product> GetProductCollection()
    {
        var collection = GetCollection<Product>("Products-v1");
        _configurator.ConfigureCollection(collection);
        return collection;
    }
    
    private IMongoDatabase GetDatabase()
    {
        var config = _configurationMonitor.CurrentValue;
        var client = _mongoConnectionFactory.GetMongoClient();
        return client.GetDatabase(config.DatabaseName);
    }
    
    private IMongoCollection<T> GetCollection<T>(string collectionName)
    {
        var database = GetDatabase();
        return database.GetCollection<T>(collectionName);
    }
}
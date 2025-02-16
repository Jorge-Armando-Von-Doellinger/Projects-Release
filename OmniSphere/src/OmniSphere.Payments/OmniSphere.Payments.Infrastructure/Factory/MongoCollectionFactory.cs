using MongoDB.Driver;
using OmniSphere.Payments.Core.Entity;

namespace OmniSphere.Payments.Infrastructure.Factory;

public class MongoCollectionFactory
{
    private readonly MongoConnectionFactory _factory;

    public MongoCollectionFactory(MongoConnectionFactory factory)
    {
        _factory = factory;
    }   
    internal IMongoDatabase GetDatabase() => _factory.GetMongoClient().GetDatabase("OmniSpheres-Payments");
    internal IMongoCollection<Payment> GetPaymentCollection() => GetDatabase().GetCollection<Payment>("Payments");
    private IMongoCollection<T> GetCollection<T>(string collectionName) => GetDatabase().GetCollection<T>(collectionName);
}
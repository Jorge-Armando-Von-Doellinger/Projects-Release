using MongoDB.Driver;
using OmniSphere.Products.Core.Entity;

namespace OmniSphere.Products.Infrastructure.Configurator;

public class ProductCollectionConfigurator
{

    internal void ConfigureCollection(IMongoCollection<Product> collection)
    {
        var EanCodeIndex = Builders<Product>.IndexKeys.Ascending(p => p.ProductEanCode);
        var NameIndex = Builders<Product>.IndexKeys.Ascending(p => p.ProductName);
        var indexOptions = new CreateIndexOptions { Unique = true };
        collection.Indexes.CreateOne(NameIndex, indexOptions);
        collection.Indexes.CreateOne(EanCodeIndex, indexOptions);
    }
}
using MongoDB.Driver;
using OmniSphere.Products.Core.Entity;
using OmniSphere.Products.Core.Interfaces.Repository;
using OmniSphere.Products.Infrastructure.Exceptions;
using OmniSphere.Products.Infrastructure.Factory;
using OmniSphere.Products.Infrastructure.Services;

namespace OmniSphere.Products.Infrastructure.Implementations.Repository;

public class ProductRepository : IProductRepository
{
    private readonly IMongoCollection<Product> _productCollection;
    private readonly TransactionService _transaction;

    public ProductRepository(IMongoCollection<Product> productCollection, TransactionService transaction)
    {
        _productCollection = productCollection;
        _transaction = transaction;
    }
    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        var docs = await _productCollection.FindAsync(doc => true);
        return await docs.ToListAsync();
    }

    public async Task<IEnumerable<Product>> GetByNameAsyncAsync(string name)
    {
        var doc = await _productCollection.FindAsync(doc => doc.ProductName.Contains(name));
        return await doc.ToListAsync();
    }

    public async Task<Product> GetByCodeAsync(string eanCode)
    {
        var doc = await _productCollection.FindAsync(doc => doc.ProductEanCode.Equals(eanCode));
        return await doc.FirstOrDefaultAsync();
    }

    public async Task<Product> GetByIdAsync(string id)
    {
        var doc = await _productCollection.FindAsync(doc => doc.Id.Equals(id));
        return await doc.FirstOrDefaultAsync();
    }

    public async Task CreateAsync(Product product)
    {
        try
        {
            await _transaction.ExecuteTransaction(async (session) =>
            {
                await _productCollection.InsertOneAsync(session, product);
            });
        }
        catch (MongoWriteException)
        {
            throw new ProductAlreadyExistsException("Product already exists");
        }
    }

    public async Task UpdateAsync(Product product)
    {
        var entity = await _productCollection.FindAsync(doc => doc.ProductEanCode.Equals(product.ProductEanCode));
        product.Id = entity.First().Id;
        await _transaction.ExecuteTransaction(async (session) =>
        {
            await _productCollection.ReplaceOneAsync(session, 
                doc => doc.ProductEanCode.Equals(product.ProductEanCode),
                product);
        });
    }

    public async Task DeleteAsync(string productId)
    {
        await _productCollection.DeleteOneAsync(doc => doc.Id.Equals(productId));
    }
}
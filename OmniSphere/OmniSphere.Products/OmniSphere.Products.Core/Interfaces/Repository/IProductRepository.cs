using System.ComponentModel.DataAnnotations;
using OmniSphere.Products.Core.Attributes;
using OmniSphere.Products.Core.Entity;

namespace OmniSphere.Products.Core.Interfaces.Repository;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetAllAsync();
    Task<IEnumerable<Product>> GetByNameAsyncAsync([MinLength(3)] string name);
    Task<Product> GetByCodeAsync([EanCodeLength] string eanCode);
    Task<Product> GetByIdAsync(string id);
    Task CreateAsync(Product product);
    Task UpdateAsync(Product product);
    Task DeleteAsync(string productId);
}
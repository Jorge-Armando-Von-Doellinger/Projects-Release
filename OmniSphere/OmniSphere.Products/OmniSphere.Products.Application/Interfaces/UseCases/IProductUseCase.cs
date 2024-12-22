using OmniSphere.Products.Application.DTOs;

namespace OmniSphere.Products.Application.Interfaces.UseCases;

public interface IProductUseCase
{
    Task AddProductAsync(ProductDTO product);
    Task UpdateProductAsync(ProductDTO product);
    Task DeleteProductAsync(string id);
    Task<ProductDTO> GetProductByIdAsync(string id);
    Task<IEnumerable<ProductDTO>> GetProductsAsync();
    Task<IEnumerable<ProductDTO>> GetProductsByNameAsync(string name);
    Task<ProductDTO> GetProductByCodeAsync(string eanCode);
}
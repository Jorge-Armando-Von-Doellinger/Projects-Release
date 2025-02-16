using OmniSphere.Products.Application.DTOs;
using OmniSphere.Products.Application.Interfaces.UseCases;
using OmniSphere.Products.Application.Mapper;
using OmniSphere.Products.Core.Interfaces.Repository;

namespace OmniSphere.Products.Application.UseCases;

public class ProductUseCase : IProductUseCase
{
    private readonly IProductRepository _repository;
    private readonly ProductMapper _mapper;

    public ProductUseCase(IProductRepository repository, ProductMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public async Task AddProductAsync(ProductDTO product)
    {
        var entity = _mapper.Map(product);
        await _repository.CreateAsync(entity);
    }

    public async Task UpdateProductAsync(ProductDTO product)
    {
        var entity = _mapper.Map(product);
        await _repository.UpdateAsync(entity);
    }

    public async Task DeleteProductAsync(string id)
    {
        await _repository.DeleteAsync(id);
    }

    public async Task<ProductDTO> GetProductByIdAsync(string id)
    {
        var entity = await _repository.GetByIdAsync(id);
        var output = _mapper.Map(entity);
        return output;
    }

    public async Task<IEnumerable<ProductDTO>> GetProductsAsync()
    {
        var entities = await _repository.GetAllAsync();
        var output = _mapper.Map(entities);
        return output;
    }

    public async Task<IEnumerable<ProductDTO>> GetProductsByNameAsync(string name)
    {
        var entities = await _repository.GetByNameAsyncAsync(name);
        var output = _mapper.Map(entities);
        return output;
    }

    public async Task<ProductDTO> GetProductByCodeAsync(string eanCode)
    {
        var entity = await _repository.GetByCodeAsync(eanCode);
        var output = _mapper.Map(entity);
        return output;
    }
}
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using OmniSphere.Products.Application.Interfaces.UseCases;
using OmniSphere.Products.Application.Mapper;
using OmniSphere.Products.Grpc.ProtoMapper;

namespace OmniSphere.Products.Grpc.Services;

public class ProductService : GrpcProductServices.GrpcProductServicesBase
{
    private readonly IProductUseCase _useCase;
    private readonly ProductProtoMapper _mapper;

    public ProductService(IProductUseCase useCase, ProductProtoMapper mapper)
    {
        _useCase = useCase;
        _mapper = mapper;
    }
    public override async Task<Empty> AddProduct(ProductProtoModel request, ServerCallContext context)
    {
        var dto = _mapper.MapToProductDTO(request);
        await _useCase.AddProductAsync(dto);
        return new Empty();
    }

    public override async Task<GetListProductProtoModel> GetProductsByName(ProductName request, ServerCallContext context)
    {
        var dtos = await _useCase.GetProductsByNameAsync(request.ProductName_);
        var protoModels = _mapper.MapToListProductProtoModels(dtos);
        return protoModels;
    }

    public override async Task<Empty> DeleteProduct(ProductCode request, ServerCallContext context)
    {
        await _useCase.DeleteProductAsync(request.Code);
        return new Empty();
    }

    public override async Task<Empty> UpdateProduct(ProductProtoModel request, ServerCallContext context)
    {
        var dto = _mapper.MapToProductDTO(request);
        await _useCase.UpdateProductAsync(dto);
        return new Empty();
    }

    public override async Task<GetListProductProtoModel> GetProducts(Empty request, ServerCallContext context)
    {
        var dtos = await _useCase.GetProductsAsync();
        var protoModels = _mapper.MapToListProductProtoModels(dtos);
        return protoModels;
    }

    public override async Task<ProductProtoModel> GetProductByCode(ProductCode request, ServerCallContext context)
    {
        var dto = await _useCase.GetProductByCodeAsync(request.Code);
        var protoModel = _mapper.MapToProductProtoModel(dto);
        return protoModel;
    }

    public override async Task<ProductProtoModel> GetProductById(ProductId request, ServerCallContext context)
    {
        var product = await _useCase.GetProductByIdAsync(request.Id);
        var protoModel = _mapper.MapToProductProtoModel(product);
        return protoModel;
    }
}
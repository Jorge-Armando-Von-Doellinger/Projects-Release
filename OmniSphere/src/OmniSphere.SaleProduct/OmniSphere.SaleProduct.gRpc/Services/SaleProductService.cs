using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using OmniSphere.SaleProduct.Application.Interfaces.UseCase;

namespace OmniSphere.SaleProduct.gRpc.Services;

public class SaleProductService : gRpc.SaleProductService.SaleProductServiceBase
{
    private readonly ISaleProductUseCase _useCase;

    public SaleProductService(ISaleProductUseCase useCase)
    {
        _useCase = useCase;
    }
    public override async Task<Empty> SaleProduct(SaleProductMessage request, ServerCallContext context)
    {
        await _useCase.SaleProductAsync(new () { 
            ProductId = request.ProductId, 
            Quantity = request.Quantity, 
            UserId = request.UserId });
        return new Empty();
    }
}
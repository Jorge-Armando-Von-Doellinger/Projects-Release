using OmniSphere.SaleProduct.Core.Entity;

namespace OmniSphere.SaleProduct.Application.Interfaces.UseCase;

public interface ISaleProductUseCase
{
    Task SaleProductAsync(ProductSale sale);
}
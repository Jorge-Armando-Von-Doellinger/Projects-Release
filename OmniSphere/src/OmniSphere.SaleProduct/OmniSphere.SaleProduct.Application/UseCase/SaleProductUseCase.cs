using OmniSphere.SaleProduct.Application.Interfaces.UseCase;
using OmniSphere.SaleProduct.Core.Entity;
using OmniSphere.SaleProduct.Core.Interfaces.External_Services;

namespace OmniSphere.SaleProduct.Application.UseCase;

public class SaleProductUseCase : ISaleProductUseCase
{
    private readonly IPaymentExternalService _paymentExternalService;
    private readonly IProductExternalService _productExternalService;

    public SaleProductUseCase(IPaymentExternalService paymentExternalService,
        IProductExternalService productExternalService)
    {
        _paymentExternalService = paymentExternalService;
        _productExternalService = productExternalService;
    }
    public async Task SaleProductAsync(ProductSale sale)
    {
        var details = await _productExternalService.GetProductPriceById(sale.ProductId);
        if(!details.HasValue) throw new KeyNotFoundException("ProductId not found");
        if(details.Value.Quantity < sale.Quantity) throw new InvalidDataException("Quantity is unavailable");
        var totalAmount = (details.Value.Price * details.Value.Quantity);
        var success = await _paymentExternalService.ExecutePaymentAsync(sale.UserId, totalAmount);
        if(success) await _productExternalService.RemoveProductAsync(sale.ProductId, sale.Quantity);
    }
}
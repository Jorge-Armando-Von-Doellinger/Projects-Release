namespace OmniSphere.SaleProduct.Core.Interfaces.External_Services;

public interface IProductExternalService
{
    Task<(double Price, int Quantity)?> GetProductPriceById(string productId);
    Task RemoveProductAsync(string productId, int quantity);
    
}
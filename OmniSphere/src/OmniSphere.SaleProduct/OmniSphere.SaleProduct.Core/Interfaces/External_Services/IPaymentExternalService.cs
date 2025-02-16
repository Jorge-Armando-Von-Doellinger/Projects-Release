namespace OmniSphere.SaleProduct.Core.Interfaces.External_Services;

public interface IPaymentExternalService
{
    Task<bool> ExecutePaymentAsync(string userId, double amount, string? message = null);
}
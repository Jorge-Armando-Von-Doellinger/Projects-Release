namespace OmniSphere.Products.Core.Interfaces.External_Services;

public interface IInventoryService
{
    Task UpdateItemAsync();
    Task AddItemAsync();
    Task RemoveItemAsync();
    Task UpdateStockAsync();
}
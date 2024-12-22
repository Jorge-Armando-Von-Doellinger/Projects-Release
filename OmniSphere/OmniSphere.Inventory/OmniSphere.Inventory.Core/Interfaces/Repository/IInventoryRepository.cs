namespace OmniSphere.Inventory.Core.Interfaces.Repository;

public interface IInventoryRepository
{
    Task AddInventoryAsync(Entity.Inventory inventory);
    Task UpdateInventoryAsync(Entity.Inventory inventory);
    Task DeleteInventoryAsync(Entity.Inventory inventory);
    Task<Entity.Inventory> GetInventoryByIdAsync(string id);
    Task<List<Entity.Inventory>> GetInventoriesAsync();
    Task<List<Entity.Inventory>> GetInventoriesByProductCodeAsync(string code);
}
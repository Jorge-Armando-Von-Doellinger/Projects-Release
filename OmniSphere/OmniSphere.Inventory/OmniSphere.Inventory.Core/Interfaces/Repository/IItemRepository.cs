using OmniSphere.Inventory.Core.Entity;
using OmniSphere.Inventory.Core.Summary;

namespace OmniSphere.Inventory.Core.Interfaces.Repository;

public interface IItemRepository
{
    Task<IEnumerable<ItemSummary>> GetItemsAsync();
    Task<ItemSummary> GetItemAsync(string id);
    Task AddItemAsync(Item item);
    Task UpdateItemAsync(Item item);
    Task DeleteItemAsync(string id);
}
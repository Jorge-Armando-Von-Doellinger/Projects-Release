using OmniSphere.Inventory.Core.Entity;
using OmniSphere.Inventory.Core.Interfaces.Repository;
using OmniSphere.Inventory.Core.Summary;

namespace OmniSphere.Inventory.Infrastructure.Implementations.Repository;

public class ItemRepository : IItemRepository
{
    public async Task<IEnumerable<ItemSummary>> GetItemsAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<ItemSummary> GetItemAsync(string id)
    {
        throw new NotImplementedException();
    }

    public async Task AddItemAsync(Item item)
    {
        throw new NotImplementedException();
    }

    public async Task UpdateItemAsync(Item item)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteItemAsync(string id)
    {
        throw new NotImplementedException();
    }
}
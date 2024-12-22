using OmniSphere.Inventory.Core.Entity;
using OmniSphere.Inventory.Core.Summary;

namespace OmniSphere.Inventory.Core.Extensions;

public static class ItemExtension
{
    public static ItemSummary ToItemSummary(this Item item)
    {
        return new()
        {
            Item = item,
            FullPrice = item.Price * item.Quantity
        };
    }

    public static IEnumerable<ItemSummary> ToItemSummaries(this IEnumerable<Item> items)
    {
        return items.Select(ToItemSummary);
        
    }
}
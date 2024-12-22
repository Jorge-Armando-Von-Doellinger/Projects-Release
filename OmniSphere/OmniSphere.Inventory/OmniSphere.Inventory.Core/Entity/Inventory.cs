using OmniSphere.Inventory.Core.Summary;

namespace OmniSphere.Inventory.Core.Entity;

public class Inventory : BaseEntity
{
    public IEnumerable<ItemSummary> Summaries { get; set; }
}
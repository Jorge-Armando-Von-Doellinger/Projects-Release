namespace OmniSphere.Inventory.Core.Entity;

public class Item : BaseEntity
{
    public required string Name { get; set; }
    public required int Quantity { get; set; }
    public required double Price { get; set; }
    public required string EanCode { get; set; }
}
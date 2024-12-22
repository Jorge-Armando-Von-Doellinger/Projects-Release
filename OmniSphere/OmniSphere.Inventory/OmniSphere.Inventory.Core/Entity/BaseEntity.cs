namespace OmniSphere.Inventory.Core.Entity;

public class BaseEntity
{
    public string Id { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? DeletedAt { get; private set; }
    public bool IsDeleted { get; private set; }

    public void SetDeleted()
    {
        IsDeleted = true;
        DeletedAt = DateTime.UtcNow;
    }
}
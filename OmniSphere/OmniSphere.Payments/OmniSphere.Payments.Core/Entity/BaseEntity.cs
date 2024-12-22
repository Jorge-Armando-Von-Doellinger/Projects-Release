namespace OmniSphere.Payments.Core.Entity;

public class BaseEntity
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public DateTime DeletedAt { get; private set; } = DateTime.UtcNow;
    private bool isDeleted;

    public bool IsDeleted
    {
        get => isDeleted;
        set
        {
            if (value == false) return;
            DeletedAt = DateTime.UtcNow;
            isDeleted = value;
        }
    }
}
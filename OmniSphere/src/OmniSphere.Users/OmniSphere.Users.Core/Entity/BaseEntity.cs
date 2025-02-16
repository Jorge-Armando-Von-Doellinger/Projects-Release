namespace OmniSphere.Users.Core.Entity;

public class BaseEntity
{
    public string Id { get; init; } = Guid.NewGuid().ToString();
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime DeletedAt { get; private set; }
    public bool IsDeleted { get; private set; }
    public void SetDeleted()
    {
        IsDeleted = true;
        DeletedAt = DateTime.UtcNow;
    }
}
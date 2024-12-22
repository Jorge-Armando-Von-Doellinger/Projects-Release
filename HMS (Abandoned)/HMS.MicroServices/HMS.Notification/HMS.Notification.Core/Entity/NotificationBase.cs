namespace HMS.Notification.Core.Entity;

public class NotificationBase
{
    public string Id { get; set; } = Guid.NewGuid().ToString();  
    public string Title { get; set; }   
    public string Content { get; set; } 
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime ModifiedAt { get; set; } = DateTime.Now;
}
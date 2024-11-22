using HMS.Notification.Core.Entity;

namespace HMS.Notification.Core.Interfaces.Repository;

public interface INotificationRepository
{
    Task AddAsync(NotificationBase notification);
    Task UpdateAsync(NotificationBase notification);
    Task DeleteAsync(string id);
    Task<NotificationBase> GetByIdAsync(string id);
    Task<List<NotificationBase>> GetAllAsync();
    
}
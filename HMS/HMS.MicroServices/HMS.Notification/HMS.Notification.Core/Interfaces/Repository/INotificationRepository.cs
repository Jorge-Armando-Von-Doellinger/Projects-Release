using HMS.Notification.Core.Entity;

namespace HMS.Notification.Core.Interfaces.Repository;

public interface INotificationRepository
{
    Task AddAsync(NotificationEntity notification);
    Task UpdateAsync(NotificationEntity notification);
    Task DeleteAsync(string id);
    Task<NotificationEntity> GetByIdAsync(string id);
    Task<List<NotificationEntity>> GetAllAsync();
    
}
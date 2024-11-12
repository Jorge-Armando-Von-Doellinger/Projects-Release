using HMS.Notification.Core.Entity;

namespace HMS.Notification.Core.Interfaces.Services;

public interface INotificationService
{
    void Send(NotificationEntity notification, string emailDestine);
}
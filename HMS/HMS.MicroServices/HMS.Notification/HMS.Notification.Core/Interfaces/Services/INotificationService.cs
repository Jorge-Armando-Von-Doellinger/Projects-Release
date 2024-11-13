using HMS.Notification.Core.Entity;

namespace HMS.Notification.Core.Interfaces.Services;

public interface INotificationService
{
    Task Send(NotificationEntity notification, string emailDestine);
}
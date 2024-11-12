using HMS.Notification.Application.DTOs;

namespace HMS.Notification.Application.Interfaces;

public interface INotificationManager
{
    Task SendAsync(NotificationDTO notification, string emailDestine);
    Task ReSendAsync(NotificationDTO notification, string emailDestine);
    Task RemoveAsync(string id);
}
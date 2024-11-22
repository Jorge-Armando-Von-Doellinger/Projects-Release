using HMS.Notification.Application.DTOs;

namespace HMS.Notification.Application.Interfaces;

public interface INotificationManager
{
    Task SendAsync(NotificationByEmailDto notificationDto);
}
using HMS.Notification.Application.DTOs;
using HMS.Notification.Core.Entity;

namespace HMS.Notification.Application.Mapper;

public sealed class NotificationMapper
{
    public NotificationByEmail MapToEntity(NotificationByEmailDto notificationDto)
    {
        return new()
        {
            Content = notificationDto.Content,
            Title = notificationDto.Title,
            Email = notificationDto.Email,
        };
    }
}
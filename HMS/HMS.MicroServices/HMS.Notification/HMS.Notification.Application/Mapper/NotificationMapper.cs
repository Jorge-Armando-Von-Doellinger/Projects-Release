using HMS.Notification.Application.DTOs;
using HMS.Notification.Core.Entity;

namespace HMS.Notification.Application.Mapper;

public sealed class NotificationMapper
{
    public NotificationEntity MapToEntity(NotificationDTO notification)
    {
        return new()
        {
            Content = notification.Content,
            Title = notification.Title,
        };
    }
    internal NotificationEntity MapToEntity(NotificationUpdateDTO notification)
    {
        return new()
        {
            Id = notification.Id,
            Content = notification.Content,
            Title = notification.Title,
        };
    }
}
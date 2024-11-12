using HMS.Notification.Application.DTOs;
using HMS.Notification.gRPC.Protos;

namespace HMS.Notification.gRPC.Mapper;

public sealed class ProtoDtoMapper
{
    internal NotificationDTO MapToDto(NotificationProtoDto notificationProtoDto)
    {
        return new()
        {
            Content = notificationProtoDto.Content,
            Title = notificationProtoDto.Title
        };
    }
}
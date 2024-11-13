using HMS.Notification.Application.DTOs;
using HMS.Notification.Application.Interfaces;
using HMS.Notification.Application.Mapper;
using HMS.Notification.Core.Interfaces.Repository;
using HMS.Notification.Core.Interfaces.Services;

namespace HMS.Notification.Application.Manager;

public sealed class NotificationManager : INotificationManager
{
    private readonly INotificationRepository _repository;
    private readonly INotificationService _notificationService;
    private readonly NotificationMapper _mapper;

    public NotificationManager(INotificationRepository repository, INotificationService notificationService, NotificationMapper mapper)
    {
        _repository = repository;
        _notificationService = notificationService;
        _mapper = mapper;
    }
    public async Task SendAsync(NotificationDTO notification, string emailDestine)
    {
        var entity = _mapper.MapToEntity(notification);
        await _notificationService.Send(entity, emailDestine);
        await _repository.AddAsync(entity);
    }

    public async Task ReSendAsync(NotificationDTO notification, string emailDestine)
    {
        var entity = _mapper.MapToEntity(notification);
        await _repository.UpdateAsync(entity);
    }

    public async Task RemoveAsync(string id)
    {
        await _repository.DeleteAsync(id);
    }
}
using System.Text;
using System.Text.Json;
using HMS.Notification.Application.DTOs;
using HMS.Notification.Application.Interfaces;
using HMS.Notification.Core.Interfaces.Messaging;
using Microsoft.Extensions.DependencyInjection;

namespace HMS.Notification.Application.Processor;

public sealed class MessageProcessor : IMessageProcessor
{
    private readonly IServiceProvider _serviceProvider;
    private readonly INotificationManager _notificationManager;

    public MessageProcessor(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }


    public async Task Process(byte[] message)
    {
        var scope = _serviceProvider.CreateAsyncScope();
        var manager = scope.ServiceProvider.GetService<INotificationManager>();
        var options = new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true,
        };
        var json = Encoding.UTF8.GetString(message);
        var notificationDto = JsonSerializer.Deserialize<NotificationByEmailDto>(json, options);
        await manager.SendAsync(notificationDto);
    }

    public Task BatchProcess(List<byte[]> messages)
    {
        throw new NotImplementedException();
    }
}
using HMS.Payments.Application.Interfaces.Services;
using HMS.Payments.Core.Interfaces.Messaging;

namespace HMS.Payments.Application.Services;

public sealed class NotificationService : INotificationService
{
    private readonly IMessagePublisher _publisher;

    public NotificationService(IMessagePublisher publisher)
    {
        _publisher = publisher;
    }
    public async Task SendNotificationAsync(string title, string message, string email)
    {
        var messageObj = new { title, message, email };
        await _publisher.PublishAsync(messageObj, "", "", "");
    }
}
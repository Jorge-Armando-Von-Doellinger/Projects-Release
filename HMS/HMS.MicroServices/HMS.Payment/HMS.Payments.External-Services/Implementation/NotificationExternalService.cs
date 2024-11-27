using HMS.Payments.Core.Interfaces.External_Services;
using HMS.Payments.Core.Interfaces.Messaging;
using HMS.Payments.External_Services.Settings;
using Microsoft.Extensions.Options;

namespace HMS.Payments.External_Services.Implementation;

public class NotificationExternalService : INotificationExternalService
{
    private readonly IOptionsMonitor<NotificationSettings> _settings;
    private readonly IMessagePublisher _publisher;

    public NotificationExternalService(IOptionsMonitor<NotificationSettings> settings, IMessagePublisher publisher)
    {
        _settings = settings;
        _publisher = publisher;
    }
    public async Task SendEmail(string content, string title, string email)
    {
        var settings = _settings.CurrentValue;
        await _publisher.PublishAsync(new { content, title, email }, 
            settings.Exchange, 
            settings.Queue,
            string.Empty);
    }
}
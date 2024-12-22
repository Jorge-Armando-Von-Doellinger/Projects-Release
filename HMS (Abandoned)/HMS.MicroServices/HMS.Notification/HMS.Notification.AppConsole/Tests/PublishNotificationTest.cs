using DotNetEnv;
using HMS.Notification.Application.DTOs;
using HMS.Notification.Core.Interfaces.Messaging;
using HMS.Notification.Messaging.Settings;
using Microsoft.Extensions.Options;

namespace HMS.Notification.AppConsole.Tests;

public sealed class PublishNotificationTest
{
    private readonly IMessagePublisher _publisher;
    private readonly IOptionsMonitor<MessagingSettings> _settings;

    public PublishNotificationTest(IMessagePublisher publisher, IOptionsMonitor<MessagingSettings> settings)
    {
        _publisher = publisher;
        _settings = settings;
    }

    public async Task Publish()
    {
        var x = 100;
        while (x != 0)
        {
            var settings = _settings.CurrentValue;
            var dto = new NotificationByEmailDto()
            {
                Content = "Batatinha quando nasce, se esparrama pelo chão!",
                Title = "Teste",
                Email = Env.GetString("EmailTest")
            };
            await _publisher.PublishAsync(dto,settings.Exchange,
                settings.Queues
                    .FirstOrDefault(x => x.Key == "Notification").Value,
                string.Empty
                );
            x--;
        }
    }
}
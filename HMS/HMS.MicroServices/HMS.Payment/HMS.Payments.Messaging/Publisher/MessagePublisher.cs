using HMS.Payments.Core.Interfaces.Messaging;
using HMS.Payments.Messaging.Context;
using HMS.Payments.Messaging.Settings;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using HMS.Payments.Core.Data;

namespace HMS.Payments.Messaging.Publisher
{
    public sealed class MessagePublisher : IMessagePublisher
    {
        private readonly IModel _channel;
        private readonly MessagingSettings _settings;
        private readonly JsonSerializerOptions jsonOptions = new ()
        {
            PropertyNamingPolicy = null
        };
        public MessagePublisher(IModel channel, RabbitContext context, IOptionsMonitor<MessagingSettings> messagingSettings)
        {
            _channel = channel;
            _settings = messagingSettings.CurrentValue;
        }

        public async Task PublishAsync<T>(T message, string exchange, string queue, string routingkey)
        {
            await Task.Run(() =>
            {
                var serialized = JsonSerializer.Serialize(message, jsonOptions);
                var bytes = Encoding.UTF8.GetBytes(serialized);
                _channel.BasicPublish(exchange, routingkey, null, bytes);
            });
            
        }

        public async Task ReRepublishAsync(MessageData message, string exchange, string queue, string routingkey)
        {
            try
            {
                await Task.Run(() =>
                {
                    message.RetryCount++; 
                    var serialized = JsonSerializer.Serialize(message, jsonOptions);
                    var bytes = Encoding.UTF8.GetBytes(serialized);
                    _channel.BasicPublish(exchange, routingkey, null, bytes);
                });
            }
            catch
            {
                
            }
        }
    }
}

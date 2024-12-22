using Nuget.Client.MessagingSettings;
using Nuget.MessagingUtilities;
using Nuget.MessagingUtilities.MessageSettings;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace HMS.Messaging.Tests
{
    internal class ResponseQueueTest
    {
        public async Task ResponseQueue(IModel channel)
        {
            var configureRoutes = new ConfigureResponseRoutings();
            var clientSettings = new ClientMessagingSettings();

            channel.QueueDeclare(queue: ResponseSettings.Queue,
                                durable: true);
            channel.QueueBind(queue: ResponseSettings.Queue,
                            exchange: ResponseSettings.Exchange,
                            routingKey: configureRoutes
                                .GetResponseKey(baseResponse: clientSettings.ResponseBase));
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (sender, args) =>
            {
                var messageBytes = Encoding.UTF8.GetString(args.Body.ToArray());
                var message = JsonSerializer.Deserialize<Message>(messageBytes);
                var message2 = JsonSerializer.Serialize(messageBytes);
                
            };
            channel.BasicConsume(ResponseSettings.Queue,
                true,
                "",
                false,
                false,
                null,
                    consumer);
        }
    }
}

using HMS.ContractsMicroService.Core.Data;
using HMS.ContractsMicroService.Core.Interfaces.Messaging;
using HMS.ContractsMicroService.Messaging.Connect;
using HMS.ContractsMicroService.Messaging.Services;
using Microsoft.Extensions.DependencyInjection;
using Nuget.Settings;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace HMS.ContractsMicroService.Messaging.Listener
{
    public sealed class MessageListener : IMessageListener
    {
        private readonly IModel _model;
        public MessageListener(IServiceProvider provider)
        {
            var service = provider.CreateScope().ServiceProvider.GetRequiredService<ConnectMessaging>();
            _model = service.Connect();
        }
        public async Task StartListener(Func<MessagingData, Task> action)
        {
            var settings = AppSettings.CurrentSettings!.RabbitMq;
            ArgumentNullException.ThrowIfNull(settings);
            await MessagingConfigureService.ConfigureQueue(_model, settings, true);
            var consumer = new EventingBasicConsumer(_model);
            consumer.Received += async (model, ea) =>
            {
                var bytes = ea.Body.ToArray();
                var data = Encoding.UTF8.GetString(bytes);
                var messagingData = new MessagingData();
                try
                {
                    messagingData.SetData(data, ea.RoutingKey.ToString());
                    await action(messagingData);
                    _model.BasicAck(ea.DeliveryTag, false);
                }
                catch(Exception ex) 
                {
                    Console.WriteLine(ex.Message);
                    _model.BasicNack(ea.DeliveryTag, false, true);
                }
            };
            _model.BasicConsume(settings.Queue, false, consumer );
            ///Console.WriteLine("Finalizado?");
        }
    }
}

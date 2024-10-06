using HMS.ContractsMicroService.Core.Data;
using HMS.ContractsMicroService.Core.Interfaces.Messaging;
using HMS.ContractsMicroService.Messaging.Connect;
using Nuget.Settings;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace HMS.ContractsMicroService.Messaging.Listener
{
    public sealed class MessageListener : IMessageListener
    {
        private readonly IModel _model;
        public MessageListener(ConnectMessaging connect)
        {
            _model = connect.Connect();
        }
        public async Task StartListener(Func<MessagingData, Task> action)
        {
            var settings = AppSettings.CurrentSettings.RabbitMq;
            ArgumentNullException.ThrowIfNull(settings);
            var consumer = new AsyncEventingBasicConsumer(_model);
            consumer.Received += async (model, ea) =>
            {
                Console.WriteLine("Started");
                var bytes = ea.Body.ToArray();
                var data = Encoding.UTF8.GetString(bytes);
                try
                {
                    var messagingData = new MessagingData();
                    messagingData.SetData(data, ea.RoutingKey);
                    await action(messagingData);
                }
                catch
                {
                    Console.WriteLine("Erro ao processar a mensagem!");
                }
            };
            _model.BasicConsume(settings.Queue, false, consumer);
            await Task.CompletedTask;
        }
    }
}

using HMS.Core.Interfaces.Messaging;
using HMS.Core.Json;
using HMS.Messaging.Factorys;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Nuget.Client.MessagingSettings;
using Nuget.MessagingUtilities;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.ComponentModel;
using System.Text;
using System.Text.Json;

namespace HMS.Messaging.Listeners
{
    public class RabbitMessageListener : BackgroundService, IMessageListener
    {
        private IModel _channel;
        private IMessageProcessor<Message> _messageProcessor;
        public RabbitMessageListener(ClientChannelFactory channelFactory, IServiceProvider serviceProvider) 
        {
            _channel = channelFactory.Channel;
            var scope = serviceProvider.CreateScope();
            _messageProcessor = scope.ServiceProvider.GetRequiredService<IMessageProcessor<Message>>();
        }

        public async Task StartListener()
        {
            try
            {
                var configs = new ClientMessagingSettings();
                var consumer = new EventingBasicConsumer(_channel);
                consumer.Received += async (model, args) =>
                {
                    string data = Encoding.UTF8.GetString(args.Body.ToArray());
                    var message = await JsonService.DeserializeAsync<Message>(data);
                    Console.WriteLine(message.ID);
                    await Task.Delay(1);
                    await _messageProcessor.Process(message);


                };
                _channel.BasicConsume(configs.Queue,
                    true,
                    "",
                    false,
                    false,
                    null,
                    consumer);
                await Task.Delay(1);
            }
            catch(Exception ex)
            {
                throw;
            }

        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Task.Run(() => StartListener());
        }
    }
}

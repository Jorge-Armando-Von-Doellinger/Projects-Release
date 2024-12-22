using HMS.ContractsMicroService.Core.Data;
using HMS.ContractsMicroService.Core.Interfaces.Messaging;
using HMS.ContractsMicroService.Messaging.Connect;
using HMS.ContractsMicroService.Messaging.Exceptions;
using HMS.ContractsMicroService.Messaging.Services;
using HMS.ContractsMicroService.Messaging.Settings.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace HMS.ContractsMicroService.Messaging.Listener
{
    public sealed class MessageListener : IMessageListener
    {
        private readonly IModel _model;
        private readonly IMessagePublisher _publisher;
        private readonly JsonSerializerOptions jsonOptions = new() { IncludeFields = true };
        private readonly IMessagingSystem _settings;

        public MessageListener(ConnectMessaging connect, IServiceProvider provider, IMessagingSystem settings)
        {
            _publisher = provider.CreateScope().ServiceProvider.GetRequiredService<IMessagePublisher>();
            _model = connect.Connect();
            _settings = settings;
        }
        public async Task StartListener(Func<string, Task> action)
        {
            if(_settings.Components.Count > -1) Console.WriteLine(_settings.Components.Values.Count);
            MessagingConfigureService.ConfigureAllQueues(_model, _settings.Components.Values.ToList());
            List<IMessagingComponents>? components = _settings.Components.Values.ToList();
            var consumer = new EventingBasicConsumer(_model);
            consumer.Received += async (model, ea) =>
            {
                var bytes = ea.Body.ToArray();
                var json = Encoding.UTF8.GetString(bytes);
                try
                {
                    Console.WriteLine(json);
                    await action(json);
                    _model.BasicAck(ea.DeliveryTag, false);
                }
                /*catch (InvalidMessageException ex)
                {
                    _model.BasicReject(ea.DeliveryTag, false);
                }
                catch (AttemptLimitException ex)
                {
                    _model.BasicReject(ea.DeliveryTag, false);
                }*/
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    _model.BasicReject(ea.DeliveryTag, false);
                }
            };

            components.ForEach(component =>
            {
                Console.WriteLine(component.Queue);
                _model.BasicConsume(component.Queue, false, consumer);
            });
        }

        private object GetMessageByBytes(byte[] bytes)
        {
            var data = Encoding.UTF8.GetString(bytes);
            try
            {
                var message = JsonSerializer.Deserialize<object>(data, jsonOptions);
                return message;
            }
            catch
            {
                Console.WriteLine("N deserializou a msg");
            }
            return null;
        }
    }
}

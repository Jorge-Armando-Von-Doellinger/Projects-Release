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
            MessagingConfigureService.ConfigureAllQueues(_model, _settings.Components.Values.ToList());
            var consumer = new EventingBasicConsumer(_model);
            IMessagingComponents? components = default;
            consumer.Received += async (model, ea) =>
            {
                var bytes = ea.Body.ToArray();
                var json = Encoding.UTF8.GetString(bytes);
                // message = this.GetMessageByBytes(bytes);
                try
                {
                    /*if (message == null) throw new InvalidMessageException("Esta mensagem não foi deserializada corretamente");
                    if (message.RetryCount > 3) throw new AttemptLimitException("Todas as tentativas de processar sua mensagem NÃO tiveram êxito");
                    var messagingData = new MessagingData();
                    messagingData.SetData(message.Content.ToString(), ea.RoutingKey); // message.content é deserializado como JsonElement*/

                    await action(json);
                    _model.BasicAck(ea.DeliveryTag, false);
                }
                catch (InvalidMessageException ex)
                {
                    _model.BasicReject(ea.DeliveryTag, false);
                }
                catch (AttemptLimitException ex)
                {
                    _model.BasicReject(ea.DeliveryTag, false);
                }
                catch (Exception ex)
                {
                    //message.AddAttempt();
                    _model.BasicReject(ea.DeliveryTag, false);
                }
            };
            _model.BasicConsume(components?.Queue, false, consumer);
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

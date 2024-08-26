using Gateway.v1.Core.Messaging.Listener;
using Gateway.v1.Core.Services;
using Gateway.v1.Messaging.Configurator;
using Gateway.v1.Messaging.Factory;
using Nuget.MessagingUtilities;
using Nuget.MessagingUtilities.MessageSettings;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace Gateway.v1.Messaging.Listener
{
    public sealed class MessageListener : IMessageListener
    {
        private IModel _channel;
        private readonly ChannelConfigurator _channelConfigurator;

        private readonly ConfigureResponseRoutings responseSettings = new ConfigureResponseRoutings();
        public MessageListener(ChannelFactory factory, ChannelConfigurator channelConfigurator)
        {
            _channel = factory.Channel;
            _channelConfigurator = channelConfigurator;
        }
        public async Task<object> GetMessage(string baseResponse, Guid messageID)
        {
            var tcs = new TaskCompletionSource<object>();

            string routingKey = responseSettings.GetResponseKey(baseResponse);
            await _channelConfigurator.ConfigureResponse(_channel, routingKey);

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += async (sender, args) =>
            {
                if (args.RoutingKey.ToLower() == routingKey.ToLower() && args.RoutingKey == routingKey)
                {
                    // Depois colocar o Id nos args
                    var body = args.Body.ToArray();
                    var json = Encoding.UTF8.GetString(body);
                    var messageDesserialized = await JsonService.DeserializeAsync<Message>(json);
                    if(messageDesserialized.ID == messageID)
                    {
                        tcs.SetResult(messageDesserialized);
                        _channel.BasicAck(args.DeliveryTag, false);
                    }
                    else
                    {
                        //_channel.BasicAck(args.DeliveryTag, false);
                        _channel.BasicReject(args.DeliveryTag, true);
                    }
                }
            };

            _channel.BasicConsume(ResponseSettings.Queue,
                false,
                string.Empty,
                false,
                false,
                null,
                consumer);

            Int16 timeout = 15000; // TIME-OUT grande, em caso de erros!
            var timeoutTask = Task.Delay(timeout);
            var completedTask = await Task.WhenAny(tcs.Task, timeoutTask);
            if (completedTask == timeoutTask)
                return default;
            return await tcs.Task;
        }
    }
}


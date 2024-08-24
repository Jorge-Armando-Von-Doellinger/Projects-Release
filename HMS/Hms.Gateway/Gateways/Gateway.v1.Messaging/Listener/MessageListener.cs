using Gateway.v1.Core.Messaging.Listener;
using Gateway.v1.Core.Messaging.Settings;
using Gateway.v1.Messaging.Configurator;
using Gateway.v1.Messaging.Factory;
using Microsoft.Extensions.Hosting;
using Nuget.MessagingUtilities;
using Nuget.MessagingUtilities.MessageSettings;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;
using System.Threading.Channels;

namespace Gateway.v1.Messaging.Listener
{
    public sealed class MessageListener : IMessageListener
    {
        private readonly IModel _channel;
        private readonly ChannelConfigurator _channelConfigurator;
        public MessageListener(ChannelFactory factory, ChannelConfigurator channelConfigurator)
        {
            _channel = factory.Channel;
            _channelConfigurator = channelConfigurator;
        }
        public async Task<object> GetMessage(string baseResponse, Guid messageID)
        {
            Message message = null;
            Int16 count = 0;
            while (count < 15)
            {
                await Task.Delay(100);
                var responseSettings = new ConfigureResponseRoutings();
                var consumer = new EventingBasicConsumer(_channel);
                string routingKey = responseSettings.GetResponseKey(baseResponse);
                Console.WriteLine(routingKey);
                consumer.Received += async (sender, args) =>
                {
                    Console.WriteLine("Recieved");
                    if(args.RoutingKey.ToLower() == routingKey.ToLower())
                    {
                        var body = args.Body.ToArray();
                        var json = Encoding.UTF8.GetString(body);
                        var messageDesserialized = JsonSerializer.Deserialize<Message>(json);
                        
                        Console.WriteLine($"{messageDesserialized.ID.ToString()} \n{messageID}");
                        if(messageDesserialized.ID == messageID)
                        {
                            Console.WriteLine("ID encontrado");
                            message = messageDesserialized;
                            _channel.BasicAck(args.DeliveryTag, false);
                        }
                        else
                            Console.WriteLine("ID não encontrado");
                    }
                    else
                        Console.WriteLine("routingKey errada");
                };
                await _channelConfigurator.ConfigureResponse(_channel, routingKey);

                _channel.BasicConsume(ResponseSettings.Queue,
                    false,
                    string.Empty,
                    false,
                    false,
                    null,
                    consumer);
                count++;
            }
            return message;
        }
    }
}

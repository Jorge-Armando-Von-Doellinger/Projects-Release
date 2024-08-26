using HMS.Core.Interfaces.Messaging;
using HMS.Core.Json;
using HMS.Messaging.Configurators;
using HMS.Messaging.Factorys;
using HMS.Messaging.Tests;
using Nuget.Client.MessagingSettings;
using Nuget.MessagingUtilities;
using Nuget.MessagingUtilities.MessageSettings;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace HMS.Messaging.Publishers
{
    public class RabbitMessagePublisher : IMessagePublisher
    {
        private readonly IModel _channel;
        private readonly ClientChannelConfigurator _channelConfigurator;
        public RabbitMessagePublisher(ClientChannelFactory factory, ClientChannelConfigurator channelConfigurator) 
        { 
            _channel = factory.Channel;
            _channelConfigurator = channelConfigurator;
        }


        public async Task<bool> PublishMessage(object data)
        {
            try
            {
                var configureResponse = new ConfigureResponseRoutings();
                var clientConfig = new ClientMessagingSettings();
                string keyResponse = configureResponse.GetResponseKey(clientConfig.ResponseBase);

                Console.WriteLine(keyResponse);

                string dataSerialized = await JsonService.SerializeAsync(data);
                Console.WriteLine(dataSerialized + " < ------------- Message serialized");

                byte[] dataBytes = Encoding.UTF8.GetBytes(dataSerialized);

                _channelConfigurator.SetConfigResponse(_channel, keyResponse);
                _channel.ConfirmSelect();


                _channel.BasicPublish(exchange: ResponseSettings.Exchange,
                    routingKey: keyResponse,
                    mandatory: false,
                    basicProperties: null,
                    body: dataBytes);
                if(_channel.WaitForConfirms())
                    Console.WriteLine("Teste 1");
                return await Task.FromResult(true);
            }
            catch (Exception ex)
            {
                throw;
                return false;
            }
        }
    }
}

﻿using Gateway.v1.Core.Messaging.Publisher;
using Gateway.v1.Messaging.Configurator;
using Gateway.v1.Messaging.Factory;
using Gateway.v1.Messaging.Serializer;
using Nuget.MessagingUtilities;
using Nuget.MessagingUtilities.MessageSettings;
using RabbitMQ.Client;
using System.Text;

namespace Gateway.v1.Messaging.Publisher
{
    public sealed class RabbitMqPublisher : IMessagePublisher<IMessageSettings>
    {
        private readonly IModel _channel;
        private readonly ChannelConfigurator _channelConfigurator;
        public RabbitMqPublisher(ChannelFactory factory, ChannelConfigurator channelConfigurator)
        {
            _channel = factory.Channel;
            _channelConfigurator = channelConfigurator;

        }


        public async Task PublishMessage(object message, IMessageSettings settings)
        {
            try
            {
                if(message == null || settings == null || message.GetType() != typeof(Message))
                    throw new NullReferenceException("Dados invealidos");
                if(_channel == null)
                    throw new NullReferenceException("Channel null");
                    
                await _channelConfigurator.Configure(_channel, settings);
                string dataJson = await DataSerializer.Serialize((Message) message);
                byte[] dataBytes = Encoding.UTF8.GetBytes(dataJson);
                Console.WriteLine(dataJson + " \nData Published \n");
                _channel.ConfirmSelect();
                _channel.BasicPublish(settings.Exchange,
                    settings.CurrentKey,
                    false,
                    null,
                    dataBytes);
                if (_channel.WaitForConfirms())
                    Console.WriteLine("Foi");
                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

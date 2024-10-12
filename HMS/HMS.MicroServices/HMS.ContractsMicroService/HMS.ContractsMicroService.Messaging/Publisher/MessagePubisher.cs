using HMS.ContractsMicroService.Core.Interfaces.Messaging;
using HMS.ContractsMicroService.Core.Json;
using HMS.ContractsMicroService.Messaging.Connect;
using HMS.ContractsMicroService.Messaging.Services;
using Nuget.MessagingUtilities;
using Nuget.Settings;
using Nuget.Settings.Messaging;
using RabbitMQ.Client;
using System.Text;

namespace HMS.ContractsMicroService.Messaging.Publisher
{
    public sealed class MessagePubisher : IMessagePublisher<RabbitMqSettings>
    {
        private readonly IModel _model;
        
        public MessagePubisher(ConnectMessaging messaging)
        {
            _model = messaging.Connect();
        }
        public async Task Publish(object data, RabbitMqSettings settings)
        {
            var appSettings = AppSettings.CurrentSettings;
            if(data.GetType() != typeof(Message))
                data = new Message(data, appSettings.ApplicationName, settings.CurrentKey);

            var bytes = await GetDataBytes(data);

            await MessagingConfigureService.ConfigureQueue(_model, settings, true);
            _model.BasicPublish(settings.Exchange,
                settings.CurrentKey,
                false,
            null,
                bytes);
        }

        private async Task<byte[]> GetDataBytes(object data)
        {
            var objJson = await JsonManipulation.Serialize(data);
            var objBytes = Encoding.UTF8.GetBytes(objJson);
            return objBytes;
        }

        public async Task PublishResponse(object data)
        {
            RabbitMqSettings settings = AppSettings.CurrentSettings.RabbitMq;
            ArgumentNullException.ThrowIfNull(settings);
            await MessagingConfigureService.ConfigureQueue(_model, settings, true);
            var bytes = await GetDataBytes(data);
            _model.BasicPublish(settings.Exchange,
                settings.ResponseKey,
                false,
                null,
                bytes);
        }
    }
}

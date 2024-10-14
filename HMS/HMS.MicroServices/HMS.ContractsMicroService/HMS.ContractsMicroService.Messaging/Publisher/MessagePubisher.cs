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
    public sealed class MessagePubisher : IMessagePublisher<IMessagingSystem>
    {
        private readonly IModel _model;

        public MessagePubisher(ConnectMessaging messaging)
        {
            _model = messaging.Connect();
        }

        public async Task Publish(object data, IMessagingSystem settings)
        {
            var appSettings = IAppSettings.CurrentSettings;
            if(data.GetType() != typeof(Message))
                data = new Message(data, appSettings.ApplicationName, settings.AddKey);

            var bytes = await GetDataBytes(data);

            MessagingConfigureService.ConfigureQueue(_model, settings);

            _model.BasicPublish(settings.Exchange,
                settings.AddKey,
                false,
            null,
                bytes);
        }


        public async Task PublishResponse(object data, IMessagingSystem settings)
        {
            ArgumentNullException.ThrowIfNull(settings);
            MessagingConfigureService.ConfigureQueue(_model, settings);
            var bytes = await GetDataBytes(data);
            _model.BasicPublish(settings.Exchange,
                settings.ResponseKey,
                false,
                null,
                bytes);
        }

        private async Task<byte[]> GetDataBytes(object data)
        {
            string objJson = await JsonManipulation.Serialize(data);
            byte[] objBytes = Encoding.UTF8.GetBytes(objJson);
            return objBytes;
        }
    }
}

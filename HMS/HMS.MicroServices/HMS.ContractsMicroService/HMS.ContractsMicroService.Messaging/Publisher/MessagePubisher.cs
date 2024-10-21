using HMS.ContractsMicroService.Core.Interfaces.Messaging;
using HMS.ContractsMicroService.Core.Json;
using HMS.ContractsMicroService.Messaging.Connect;
using HMS.ContractsMicroService.Messaging.Services;
using HMS.ContractsMicroService.Messaging.Settings.Interfaces;
using RabbitMQ.Client;
using System.Text;

namespace HMS.ContractsMicroService.Messaging.Publisher
{
    public sealed class MessagePubisher : IMessagePublisher
    {
        private readonly IModel _model;
        private readonly IMessagingSystem _settings;

        public MessagePubisher(ConnectMessaging messaging, IMessagingSystem settings)
        {
            _model = messaging.Connect();
            _settings = settings;
        }

        public async Task Publish(object data, string key)
        {
            var bytes = await GetDataBytes(data);
            var components = _settings.Components.Values.FirstOrDefault(x =>
            {
                if(x.Keys.Contains(key)) return true;
                return false;
            }) ?? throw new KeyNotFoundException("routing key not founded");
            MessagingConfigureService.ConfigureQueue(_model, components);

            _model.BasicPublish(components.Exchange,
                components.Keys.First(x => x.Equals(key)),
                false,
            null,
                bytes);
        }


        public async Task PublishResponse(object data, string responseKey)
        {
            var components = _settings.Components.Values
                .FirstOrDefault(x => x.Keys.Contains(responseKey));
            ArgumentNullException.ThrowIfNull(components);
            MessagingConfigureService.ConfigureQueue(_model, components);
            var bytes = await GetDataBytes(data);
            _model.BasicPublish(components.Exchange,
                components.ResponseKey,
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

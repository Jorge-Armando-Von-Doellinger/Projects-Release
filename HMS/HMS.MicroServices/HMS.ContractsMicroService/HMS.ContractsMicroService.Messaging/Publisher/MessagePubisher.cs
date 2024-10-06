using HMS.ContractsMicroService.Core.Interfaces.Messaging;
using HMS.ContractsMicroService.Core.Json;
using HMS.ContractsMicroService.Messaging.Connect;
using HMS.ContractsMicroService.Messaging.Services;
using Nuget.Settings;
using Nuget.Settings.Messaging;
using RabbitMQ.Client;
using System.Text;

namespace HMS.ContractsMicroService.Messaging.Publisher
{
    public sealed class MessagePubisher : IMessagePublisher<RabbitMqSettings>
    {
        private readonly IModel _messaging;

        public MessagePubisher(ConnectMessaging messaging)
        {
            _messaging = messaging.Connect();
        }   
        public async Task Publish(object data, RabbitMqSettings settings)
        {
            using (_messaging)
            {
                var bytes = await GetDataBytes(data);
                _messaging.BasicPublish(settings.Exchange,
                    settings.ResponseKey,
                    false,
                    null,
                    bytes);
            }
        }

        private async Task<byte[]> GetDataBytes(object data)
        {
            var objJson = await JsonManipulation.Serialize(data);
            var objBytes = Encoding.UTF8.GetBytes(objJson);
            return objBytes;
        }

        public async Task PublishResponse(object data)
        {
            using (_messaging)
            {
                RabbitMqSettings settings = AppSettings.CurrentSettings.RabbitMq;
                ArgumentNullException.ThrowIfNull(settings);

                var bytes = await GetDataBytes(data);
                _messaging.BasicPublish(settings.Exchange,
                    settings.ResponseKey,
                    false, 
                    null,
                    bytes);
            }
        }
    }
}

using HMS.ContractsMicroService.Core.Interfaces.Messaging;
using HMS.ContractsMicroService.Core.Json;
using HMS.ContractsMicroService.Messaging.Connect;
using HMS.ContractsMicroService.Messaging.Services;
using RabbitMQ.Client;
using System.Text;

namespace HMS.ContractsMicroService.Messaging.Publisher
{
    public sealed class MessagePubisher : IMessagePublisher
    {
        private readonly CacheSettingsService _cache;
        private readonly IModel _messaging;

        public MessagePubisher(ConnectMessaging messaging, CacheSettingsService cache)
        {
            _cache = cache;
            _messaging = Task.Run(() => messaging.Connect()).Result;
        }
        public async Task Publish(object data)
        {
            var settings = _cache.GetMessagingSettings();
            using (_messaging)
            {
                var objJson = await JsonManipulation.Serialize(data);
                var objBytes = Encoding.UTF8.GetBytes(objJson);
                Console.WriteLine(settings.Exchange);
                Console.WriteLine(settings.ResponseKey);
                _messaging.BasicPublish(settings.Exchange, 
                    settings.ResponseKey,
                    false, 
                    null,
                    objBytes);
            }
        }

        public Task PublishResponse(object data)
        {
            throw new NotImplementedException();
        }
    }
}

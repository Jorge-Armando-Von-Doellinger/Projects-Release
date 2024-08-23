using Gateway.v1.Core.Messaging.Listener;
using Gateway.v1.Core.Messaging.Publisher;
using Gateway.v1.Core.Messaging.Settings;
using Nuget.Client.MessagingSettings;
using Nuget.MessagingUtilities;
using Nuget.MessagingUtilities.MessageSettings;
using System.Text.Json;

namespace Gateway.v1.Application.Services.Messaging
{
    public sealed class MessageService
    {
        public readonly IMessagePublisher<IMessageSettings> _messagePublisher;
        public readonly IMessageListener _messageListener;
        public MessageService(IMessagePublisher<IMessageSettings> messagePublisher, IMessageListener messageListener)
        {
            _messagePublisher = messagePublisher;
            _messageListener = messageListener;
            // Se eu colocar um tipo em IMessageSettings, será possivel pegar os dados de todos
        }
        public async Task<Message> PublishMessage(object data, IMessageSettings settings)
        {
            try
            {
               //var serialized = JsonSerializer.Serialize(data);
               Console.WriteLine(GatewayMessageSettings.Key +"\n \n \n");
                Message message = new Message();
                    message.Configure(data,
                    GatewayMessageSettings.Key,
                    settings.CurrentKey,
                    GatewayMessageSettings.Key);
                await _messagePublisher.PublishMessage(message, settings);
                return message; 
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<object> GetObjectAsync(string baseResponse, Guid messageID)
            => await _messageListener.GetMessage(baseResponse, messageID);
        
    }
}

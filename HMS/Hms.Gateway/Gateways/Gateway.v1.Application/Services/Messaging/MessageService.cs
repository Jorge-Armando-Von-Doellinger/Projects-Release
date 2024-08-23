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
        public IMessagePublisher<IMessageSettings> _messagePublisher;
        public MessageService(IMessagePublisher<IMessageSettings> messagePublisher)
        {
            _messagePublisher = messagePublisher;
            // Se eu colocar um tipo em IMessageSettings, será possivel pegar os dados de todos
        }
        public async Task PublishMessage(object data, IMessageSettings settings)
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
                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}

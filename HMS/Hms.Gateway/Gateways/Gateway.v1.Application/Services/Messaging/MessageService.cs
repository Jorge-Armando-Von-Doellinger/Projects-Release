using Gateway.v1.Core.Messaging.Publisher;
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
                var message = new Message()
                {

                    Content = data,
                    Destination = settings.CurrentKey,
                    ID = Guid.NewGuid(),
                    MessageFlow = new(),
                    Origin = "gateway"
                };
                message.MessageFlow.Add("gateway");
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

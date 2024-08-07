using HMS.Application.Managers;
using HMS.Core.Interfaces.Messaging;
using Nuget.Clients.DTOs.Input;
using Nuget.Clients.DTOs.Mensaging;
using Nuget.MessageObject;
using System.Text.Json;

namespace HMS.Application.MessageProcessor
{
    public class MessageProcessor : IMessageProcessor
    {
        private readonly ClientMessageManager _messageManager;
        public MessageProcessor(ClientMessageManager messageManager)
        {
            _messageManager = messageManager;
        }
        public async Task Process(Message message)
        {
            try
            {
                var configs = new ClientMessagingConfigs();
                switch(message.Destination)
                {
                    case string dest when dest == configs.AddClientKey:
                        var inputModel = JsonSerializer.Deserialize<InputModel>(message.Content);
                        if(inputModel != null)
                            await _messageManager.AddClientAsync(inputModel);
                        break;
                    case string dest when dest == configs.UpdateKey:
                        var updateModel = JsonSerializer.Deserialize<UpdateModel>(message.Content);
                        if(updateModel != null)
                            await _messageManager.UpdateClientAsync(updateModel);
                        break;
                    case string dest when dest == configs.DeleteKey:
                        long data = JsonSerializer.Deserialize<long>(message.Content);
                        if(data != 0)
                            await _messageManager.DeleteClientAsync(data);
                        break;
                    default: throw new Exception("Invalid Key Detected");
                }
                message.MessageFlow.Add($"{configs.Exchange}, {configs.Queue}, {message.Destination}");
            }
            catch
            {
                throw;
            }
        }
    }
}

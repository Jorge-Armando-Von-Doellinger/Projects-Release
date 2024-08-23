using HMS.Application.Desserialize;
using HMS.Application.Managers;
using HMS.Application.Services;
using HMS.Core.Interfaces.Messaging;
using Nuget.Client.Input;
using Nuget.Client.MessagingSettings;
using Nuget.MessagingUtilities;
using Nuget.Response;
using System.Text.Json;

namespace HMS.Application.MessageProcessor
{
    public class MessageProcessor : IMessageProcessor<Message>
    {
        private readonly ClientMessageManager _messageManager;
        public MessageProcessor(ClientMessageManager messageManager)
        {
            _messageManager = messageManager;
        }
        public async Task Process(Message message)
        {
            var response = new Response();
            var configs = new ClientMessagingSettings();
            try
            {
                switch(message.Destination)
                {
                    case string dest when dest.ToLower() == configs.AddKey.ToLower():
                        var inputModel = await JsonDeserialize.Deserialize<InputModel>(message.Content);
                        if(inputModel != null)
                           response = await _messageManager.AddClientAsync(inputModel);
                        break;
                    case string dest when dest == configs.UpdateKey:
                        var updateModel = await JsonDeserialize.Deserialize<UpdateModel>(message.Content);
                        if(updateModel != null)
                            response = await _messageManager.UpdateClientAsync(updateModel);
                        break;
                    case string dest when dest == configs.DeleteKey:
                        long data = await JsonDeserialize.Deserialize<long>(message.Content);
                        if(data != 0)
                            response = await _messageManager.DeleteClientAsync(data);
                        break;
                    default: throw new Exception("Invalid Key Detected");
                }
            }
            catch
            {
                throw;
            }
            finally
            {

                message.AddMessageFlow($"{configs.Exchange}, {configs.Queue}, {message.Destination}");
                Message messageResponse = ResponseService.CreateMessageResponse(messageRecieved: message,                                                                                success: response.Success,
                                                                                message: response.Message);
                await _messageManager.PublishResponse(messageResponse);
            }
        }
    }
}

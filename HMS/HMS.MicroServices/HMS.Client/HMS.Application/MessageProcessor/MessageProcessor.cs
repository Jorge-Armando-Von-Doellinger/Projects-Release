using HMS.Application.Desserialize;
using HMS.Application.Managers;
using HMS.Application.Services;
using HMS.Core.Interfaces.Messaging;
using Nuget.Client.Input;
using Nuget.Client.MessagingSettings;
using Nuget.MessagingUtilities;
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
            try
            {

                var configs = new ClientMessagingSettings();
                bool success = false; //Success inicia com false e, caso dê tudo certo, será atribuido o valor: true
                switch(message.Destination)
                {
                    case string dest when dest.ToLower() == configs.AddKey.ToLower():
                        InputModel inputModel = await JsonDeserialize.Deserialize<InputModel>(message.Content);
                        if(inputModel != null)
                           success = await _messageManager.AddClientAsync(inputModel);
                        break;
                    case string dest when dest == configs.UpdateKey:
                        UpdateModel updateModel = await JsonDeserialize.Deserialize<UpdateModel>(message.Content);
                        if(updateModel != null)
                            success = await _messageManager.UpdateClientAsync(updateModel);
                        break;
                    case string dest when dest == configs.DeleteKey:
                        long data = await JsonDeserialize.Deserialize<long>(message.Content);
                        if(data != 0)
                            success = await _messageManager.DeleteClientAsync(data);
                        break;
                    default: throw new Exception("Invalid Key Detected");
                }

                message.AddMessageFlow($"{configs.Exchange}, {configs.Queue}, {message.Destination}");
                Message messageResponse = ResponseService.CreateMessageResponse(messageRecieved: message,
                                                                                success: success);
                await _messageManager.PublishResponse(messageResponse);
            }
            catch
            {
                throw;
            }
        }
    }
}

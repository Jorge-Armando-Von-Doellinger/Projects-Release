﻿using HMS.Application.Managers;
using HMS.Application.Services;
using HMS.Core.Interfaces.Messaging;
using HMS.Core.Json;
using Nuget.Client.Input;
using Nuget.Client.MessagingSettings;
using Nuget.MessagingUtilities;
using Nuget.MessagingUtilities.MessageSettings;
using Nuget.Response;
using System.Text.Json;

namespace HMS.Application.MessageProcessor
{
    public sealed class MessageProcessor : IMessageProcessor<Message>
    {
        private readonly ClientMessageManager _messageManager;
        public MessageProcessor(ClientMessageManager messageManager)
        {
            _messageManager = messageManager;
        }
        public async Task Process(Message message)
        {
            // Tem muito o que melhorar...
            var response = new Response();
            var configureResponse = new ConfigureResponseRoutings();
            var configs = new ClientMessagingSettings();
            try
            {
                switch(message.Destination)
                {
                    case string dest when dest.ToLower() == configs.AddKey.ToLower():
                        var inputModel = await JsonService.DeserializeAsync<InputModel>(message.Content);
                        if(inputModel != null)
                            response = await _messageManager.AddClientAsync(inputModel);
                        break;
                    case string dest when dest == configs.UpdateKey:
                        var updateModel = await JsonService.DeserializeAsync<UpdateModel>(message.Content);
                        if(updateModel != null)
                            response = await _messageManager.UpdateClientAsync(updateModel);
                        break;
                    case string dest when dest == configs.DeleteKey:
                        long data = await JsonService.DeserializeAsync<long>(message.Content);
                        if(data != 0)
                            response = await _messageManager.DeleteClientAsync(data);
                        break;
                    default: throw new Exception("Invalid Key Detected");
                }
                message.AddMessageFlow($"{message.Destination}");
                Message messageResponse = ResponseService.CreateMessageResponse(messageRecieved: message, success: response.Success,
                                                                                message: response.Message);
                message.AddMessageFlow(configureResponse.GetResponseKey(configs.ResponseBase));
                await _messageManager.PublishResponse(message);
            }
            catch
            {
                Console.WriteLine("Erro");
                throw;
            }
        }
    }
}

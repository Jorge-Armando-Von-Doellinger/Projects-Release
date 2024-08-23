using Gateway.v1.Application.Facades;
using Gateway.v1.Application.Interfaces;
using Gateway.v1.Application.Mappers;
using Gateway.v1.Application.Services.Messaging;
using Gateway.v1.Core.Messaging.Settings;
using Nuget.Client.MessagingSettings;
using Nuget.Client.Output;
using Nuget.MessagingUtilities;

namespace Gateway.v1.Application.Managers
{
    public sealed class ClientManager : IManager
    {
        private readonly ClientFacade _clientFacade;
        private readonly MessageService _messageService;
        private ClientMessagingSettings _settings;
        public ClientManager(ClientFacade clientFacade, MessageService messageService, ClientMessagingSettings settings)
        {
            _clientFacade = clientFacade;
            _messageService = messageService;
            _settings = settings;
        }

        public async Task<bool> Add(object input)
        {
            try
            {
                _settings.CurrentKey = _settings.AddKey;
                await _messageService.PublishMessage(input, _settings);
                await Task.CompletedTask;
                return true;
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> Delete(long id)
        {
            try
            {
                _settings.CurrentKey = _settings.DeleteKey;
                await Task.Delay(10);
                await _messageService.PublishMessage(id, _settings);
                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<object> Get()
        {
            try
            {
                var response = await _clientFacade.GetClient();
                return response;
            }
            catch(Exception ex)
            {
                throw;
            };
        }

        public async Task<object> GetById(long ID)
        {
            try
            {
                var response = await _clientFacade.GetClientByID(ID); 
                return response;
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> Update(object input)
        {
            try
            {
                _settings.CurrentKey = _settings.UpdateKey;
                await _messageService.PublishMessage(input, _settings);
                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /*public async Task<List<OutputModel>> GetClientsAsync()
        {
            try
            {
                return await _clientFacade.GetClient();
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public async Task<OutputModel> GetClientByIdAsync(long ID)
        {
            try
            {
                return await _clientFacade.GetClientByID(ID);
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public async Task AddClientAsync(InputModel input)
        {
            try
            {
                _settings.CurrentKey = _settings.AddKey;
                await _messageService.PublishMessage(input, _settings);
                await Task.CompletedTask;
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public async Task UpdateClientAsync*/
    }
}

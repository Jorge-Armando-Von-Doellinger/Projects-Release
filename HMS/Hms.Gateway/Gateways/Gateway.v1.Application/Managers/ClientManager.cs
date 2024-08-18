using Gateway.v1.Application.Facades;
using Gateway.v1.Application.Services;
using Gateway.v1.Application.Services.Messaging;
using Nuget.Client.Input;
using Nuget.Client.MessagingSettings;
using Nuget.Client.Output;

namespace Gateway.v1.Application.Managers
{
    public sealed class ClientManager
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

        public async Task<List<OutputModel>> GetClientsAsync()
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

        public async Task UpdateClientAsync
    }
}

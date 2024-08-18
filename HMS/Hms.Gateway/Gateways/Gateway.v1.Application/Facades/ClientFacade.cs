using Gateway.v1.Application.Services.Clients;
using Nuget.Client.Output;

namespace Gateway.v1.Application.Facades
{
    public sealed class ClientFacade
    {
        private readonly GetClientsService _getClientsService;
        public ClientFacade(GetClientsService getClientsService)
        {
            _getClientsService = getClientsService;
        }

        public async Task<List<OutputModel>> GetClient()
            => await _getClientsService.GetClientsAsync();
        public async Task<OutputModel> GetClientByID(long ID)
            => await _getClientsService.GetClientByIdAsync(ID);
    }
}

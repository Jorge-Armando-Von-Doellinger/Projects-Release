using Gateway.v1.Application.Services.Clients;
using Nuget.Client.Output;
using Nuget.Response;

namespace Gateway.v1.Application.Facades
{
    public sealed class ClientFacade
    {
        private readonly GetClientsService _getClientsService;
        public ClientFacade(GetClientsService getClientsService)
        {
            _getClientsService = getClientsService;
        }

        public async Task<Response> GetClient()
            => await _getClientsService.GetClientsAsync();
        public async Task<Response> GetClientByID(long ID)
            => await _getClientsService.GetClientByIdAsync(ID);
    }
}

using HMS.Application.Services;
using HMS.Core.Entity;
using HMS.Core.Interfaces.Repository;
using Nuget.Clients.DTOs.Input;
using Nuget.Clients.DTOs.Output;

namespace HMS.Application.Managers
{
    public class ClientManager
    {
        private readonly IClientRepository _clientRepository;
        private readonly DataService _dataService;
        // Publisher de mensagem

        public ClientManager(IClientRepository repository, DataService dataService) 
        { 
            _clientRepository = repository;
            _dataService = dataService;
        }
        
        public async Task<List<OutputModel>> GetClientsAsync()
        {
            try
            {
                List<ClientEntity> clients = await _clientRepository.GetClientsAsync();
                return await _dataService.Mapper.Map(clients);
            }
            catch
            {
                return default;
            }
        }

        // Get não precisa de fila, ou precisa?

        public async Task<OutputModel> GetClientByIdAsync(long ID)
        {
            try
            {
                ClientEntity client = await _clientRepository.GetClientsByIdAsync(ID);
                var output = _dataService.Mapper.Map(client);
                return output;
            }
            catch
            {
                throw;
            }
        }
    }
}

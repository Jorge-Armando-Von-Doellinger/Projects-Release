using HMS.Application.Services;
using HMS.Client.Models.Output;
using HMS.Core.Interfaces.Repository;

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
        
        public OutputModel GetClientsAsync()
        {
            throw new NotImplementedException();
        }
    }
}

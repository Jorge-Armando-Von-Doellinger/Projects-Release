using HMS.Application.Services;
using HMS.Application.Tests;
using HMS.Core.Entity;
using HMS.Core.Interfaces.Messaging;
using HMS.Core.Interfaces.Repository;
using Microsoft.Extensions.DependencyInjection;
using Nuget.Client.Input;
using Nuget.Client.Output;
using Nuget.MessagingUtilities;
using System.Text.Json;

namespace HMS.Application.Managers
{
    public class ClientManager
    {
        private readonly IClientRepository _clientRepository;
        private readonly DataService _dataService;
        private readonly IServiceScope scope;
        // Publisher de mensagem

        public ClientManager(IClientRepository repository, DataService dataService, IServiceProvider serviceProvider)
        {
            _clientRepository = repository;
            _dataService = dataService;
            scope = serviceProvider.CreateScope();


        }

        
        public async Task<List<OutputModel>> GetClientsAsync()
        {
            try
            {
                //await new PublishMessage().teste(scope);
                
                List<ClientEntity> clients = await _clientRepository.GetClientsAsync();
                return await _dataService.Mapper.Map(clients);
            }
            catch
            {
                throw;
                //return default;
            }
        }

        // Get não precisa de fila, ou precisa?

        public async Task<OutputModel> GetClientByIdAsync(long ID)
        {
            try
            {
                //new PublishMessage().teste(scope);
                ClientEntity client = await _clientRepository.GetClientsByIdAsync(ID) ??
                    throw new Exception("Cliente não encontrado!");
                return await Task.FromResult(_dataService.Mapper.Map(client));
            }
            catch
            {
                return default;
            }
        }
    }
}

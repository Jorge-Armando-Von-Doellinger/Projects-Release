using HMS.Application.Services;
using HMS.Application.Tests;
using HMS.Core.Entity;
using HMS.Core.Interfaces.Messaging;
using HMS.Core.Interfaces.Repository;
using Microsoft.Extensions.DependencyInjection;
using Nuget.Client.Input;
using Nuget.Client.Output;
using Nuget.MessagingUtilities;
using Nuget.Response;
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

        
        public async Task<Response> GetClientsAsync()
        {
            var response = new Response();
            try
            {
                List<ClientEntity> clients = await _clientRepository.GetClientsAsync();
                response.Content = await _dataService.Mapper.Map(clients);
            }
            catch (Exception ex) 
            {
                response.CaseError(ex.Message);
            }
            return response;
        }

        // Get não precisa de fila, ou precisa?

        public async Task<Response> GetClientByIdAsync(long ID)
        {
            var response = new Response();
            try
            {
                //new PublishMessage().teste(scope);
                ClientEntity client = await _clientRepository.GetClientsByIdAsync(ID) ??
                    throw new Exception("Cliente não encontrado!");
                response.Content = await Task.FromResult(_dataService.Mapper.Map(client));
            }
            catch (Exception ex)
            {
                response.CaseError(ex.Message);
            }
            return response;
        }
    }
}

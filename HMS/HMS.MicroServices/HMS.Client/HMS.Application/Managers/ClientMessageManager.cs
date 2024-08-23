using HMS.Application.Services;
using HMS.Core.Entity;
using HMS.Core.Interfaces.Messaging;
using HMS.Core.Interfaces.Repository;
using Nuget.Client.Input;
using Nuget.MessagingUtilities;
using Nuget.Response;

namespace HMS.Application.Managers
{
    public class ClientMessageManager
    {
        private readonly IClientRepository _clientRepository;
        private readonly DataService _dataService;
        private readonly IMessagePublisher _messagePublisher;
        // Publisher de mensagem

        public ClientMessageManager(IClientRepository repository, DataService dataService, IMessagePublisher messagePublisher)
        {
            _clientRepository = repository;
            _messagePublisher = messagePublisher;
            _dataService = dataService;
        }

        public async Task<Response> AddClientAsync(InputModel input)
        {
            var response = new Response();
            try
            {
                ClientEntity client = _dataService.Mapper.Map(input);
                int rowsAffected = await _clientRepository.AddClientAsync(client);
                if(rowsAffected == 1)
                    return response;
                throw new Exception($"Houve um erro durante a adição do cliente. Linhas afetadas: {rowsAffected}");
            }
            catch(Exception ex) 
            {
                response.CaseError(ex.Message);
                return response;
            }
        }

        public async Task<Response> UpdateClientAsync(UpdateModel input)
        {
            var response = new Response();
            try
            {
                ClientEntity client = _dataService.Mapper.Map(input);
                int rowsAffected = await _clientRepository.UpdateClientAsync(client);
                if(rowsAffected == 1)
                    return response;
                throw new Exception("Houve um erro durante a atualização do client");
            }
            catch (Exception ex)
            {
                response.CaseError(ex.Message);
                return response;
            }
        }

        public async Task<Response> DeleteClientAsync(long ID)
        {
            var response = new Response();
            try
            {
                int rowsAffected = await _clientRepository.DeleteClientAsync(new ClientEntity() { Id = ID });
                if(rowsAffected == 1)
                    return response;
                throw new Exception("Houve um erro ao excluir o cliente");
            }
            catch (Exception ex)
            {
                response.CaseError(ex.Message);
                return response;
            }
        }

        internal async Task<bool> PublishResponse(Message message)
        {
            try
            {
                bool publishSuccess = await _messagePublisher.PublishMessage(message);
                return publishSuccess;
            }
            catch
            {
                return false;
            }
        }
    }
}

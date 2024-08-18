﻿using HMS.Application.Services;
using HMS.Core.Entity;
using HMS.Core.Interfaces.Messaging;
using HMS.Core.Interfaces.Repository;
using Nuget.Client.Input;
using Nuget.MessagingUtilities;

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

        public async Task<bool> AddClientAsync(InputModel input)
        {
            try
            {
                ClientEntity client = _dataService.Mapper.Map(input);
                int rowsAffected = await _clientRepository.AddClientAsync(client);
                if(rowsAffected == 1)
                    return true;
                throw new Exception($"Houve um erro durante a adição do cliente. Linhas afetadas: {rowsAffected}");
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> UpdateClientAsync(UpdateModel input)
        {
            try
            {
                ClientEntity client = _dataService.Mapper.Map(input);
                int rowsAffected = await _clientRepository.UpdateClientAsync(client);
                if(rowsAffected == 1)
                    return true;
                throw new Exception("Houve um erro durante a atualização do client");
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> DeleteClientAsync(long ID)
        {
            try
            {
                int rowsAffected = await _clientRepository.DeleteClientAsync(new ClientEntity() { Id = ID });
                if(rowsAffected == 1)
                    return true;
                throw new Exception("Houve um erro ao excluir o cliente");
            }
            catch
            {
                throw;
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

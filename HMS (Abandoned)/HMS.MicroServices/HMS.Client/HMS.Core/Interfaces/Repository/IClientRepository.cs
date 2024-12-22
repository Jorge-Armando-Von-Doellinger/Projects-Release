using HMS.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Core.Interfaces.Repository
{
    public interface IClientRepository
    {
        Task<List<ClientEntity>> GetClientsAsync();
        Task<ClientEntity> GetClientsByIdAsync(long ID);
        Task<int> AddClientAsync(ClientEntity client);
        Task<int> UpdateClientAsync(ClientEntity client);
        Task<int> DeleteClientAsync(ClientEntity client);
    }
}

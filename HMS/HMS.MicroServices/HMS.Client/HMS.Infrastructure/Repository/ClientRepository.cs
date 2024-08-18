using HMS.Core.Entity;
using HMS.Core.Interfaces.Repository;
using HMS.Infrastructure.DataContext;
using HMS.Infrastructure.TransactionServices;
using Microsoft.EntityFrameworkCore;


namespace HMS.Infrastructure.Repository
{
    public class ClientRepository : IClientRepository
    {
        private readonly ClientContext _context;
        private readonly ClientTransactionService _transactionService;
        public ClientRepository(ClientContext context, ClientTransactionService transactionService)
        {
            _context = context;
            _transactionService = transactionService;
        } 

        public async Task<int> AddClientAsync(ClientEntity client)
        {
            try
            {
                return await _transactionService.ExecuteTransactionAsync(_context, async () =>
                {
                    await _context.Clients.AddAsync(client);
                });
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public async Task<int> DeleteClientAsync(ClientEntity client)
        {
            try
            {
                return await _transactionService.ExecuteTransactionAsync(_context, async () =>
                {
                    await Task.FromResult(_context.Clients.Remove(client));
                });
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public async Task<List<ClientEntity>> GetClientsAsync()
        {
            try
            {
                return await _context.Clients
                    .AsNoTracking()
                    .ToListAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<ClientEntity> GetClientsByIdAsync(long ID)
        {
            try
            {
                return await _context.Clients
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == ID);
            }
            catch(Exception ex)
            {
                throw ex;
                return null;
            }
        }

        public async Task<int> UpdateClientAsync(ClientEntity client)
        {
            try
            {
                using(_context)
                {
                    ClientEntity existingClient = await _context.Clients
                        .AsNoTracking()
                        .FirstOrDefaultAsync(x => x.Id == client.Id);
                    if(existingClient == default)
                        throw new Exception("Client não encontrado");
                    return await _transactionService.ExecuteTransactionAsync(_context, async () =>
                    {
                        client.UpdateClient(existingClient);
                        _context.Clients.Update(client);
                    });
                }
            }
            catch
            {
                throw;
            }
        }
    }
}

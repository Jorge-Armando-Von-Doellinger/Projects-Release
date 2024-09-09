using HMS.Employee.Core.Entity;
using HMS.Employee.Core.Interface.Repository;
using HMS.Employee.Infrastructure.DataContext;
using HMS.Employee.Infrastructure.MessageResponse;
using HMS.Employee.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

namespace HMS.Employee.Infrastructure.Repository
{
    public sealed class ContractsRepository : IRepositoryWithEmployeeId<ContractualInformation>
    {
        private readonly ContractualInformationContext _context;
        private readonly TransactionService _transaction;
        public ContractsRepository(ContractualInformationContext context, TransactionService transaction) 
        {
            _context = context;
            _transaction = transaction;
        }

        public async Task<bool> Add(ContractualInformation entity)
        {
            try
            {
                var rowsAffected = await _transaction.Execute(_context, async () =>
                {
                    await _context.ContractualInformation.AddAsync(entity);
                });
                if (rowsAffected < 0) throw new TimeoutException(DefaultMessages.ErrorTransactionActive); // Sofreu time-out
                else if (rowsAffected > 1) throw new TimeoutException(DefaultMessages.ErrorTransaction);
                return rowsAffected == 1;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Task<bool> Delete(ContractualInformation ID)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ContractualInformation>> Get()
        {
            return await _context.ContractualInformation
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<List<ContractualInformation>> GetByEmployeeId(Guid ID)
        {
            return await _context.ContractualInformation
                .AsNoTracking()
                .Where(x => x.Id == ID)
                .ToListAsync();
        }

        public async Task<ContractualInformation> GetById(Guid ID)
        {
            return await _context.ContractualInformation
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == ID);
        }

        public async Task<bool> Update(ContractualInformation entity)
        {
            var rowsAffected = await _transaction.Execute(_context, async () =>
            {
                var contract = await _context.ContractualInformation.FindAsync(entity.Id);
                await contract.UpdateAsync(entity);
            });
            if (rowsAffected < 0) throw new TimeoutException(DefaultMessages.ErrorTransactionActive); // Sofreu time-out
            if (rowsAffected >= 0) throw new TimeoutException(DefaultMessages.ErrorTransaction);
            return rowsAffected == 1;
        }
    }
}

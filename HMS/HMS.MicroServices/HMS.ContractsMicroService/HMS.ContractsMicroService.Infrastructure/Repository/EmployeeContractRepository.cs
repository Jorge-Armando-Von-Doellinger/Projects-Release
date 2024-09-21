using HMS.ContractsMicroService.Core.Entity;
using HMS.ContractsMicroService.Core.Interfaces.Repository;
using HMS.ContractsMicroService.Core.Json;
using HMS.ContractsMicroService.Infrastructure.Context;
using HMS.ContractsMicroService.Infrastructure.Messages;
using HMS.ContractsMicroService.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

namespace HMS.ContractsMicroService.Infrastructure.Repository
{
    public sealed class EmployeeContractRepository : IEmployeeContractRepository
    {
        private readonly ContractContext _context;
        private readonly TransactionService _transaction;

        public EmployeeContractRepository(ContractContext context, TransactionService transaction)
        {
            _context = context;
            _transaction = transaction;
        }

        public async Task AddAsync(EmployeeContract entity)
        {
            Console.WriteLine(entity.JobTitle);
            await _transaction.Execute(_context, async () =>
            {   
                await _context.Entry(entity).Context.AddAsync(entity);
            });
        }

        public async Task DeleteAsync(Guid entityId)
        {
            await _transaction.Execute(_context, async () =>
            {
                var contract = _context.EmployeeContract
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.ID == entityId)
                    ?? throw new KeyNotFoundException(MessageRecords.KeyNotFounded);
            });
        }

        public async Task<List<EmployeeContract>> GetAsync()
        {
            return await _context.EmployeeContract
                .AsNoTracking()
                .Include(X => X.WorkHours)
                .OrderBy(X => X.UpdatedAt)
                .ToListAsync();
        }

        public async Task<EmployeeContract> GetByIdAsync(Guid entityId)
        {
            return await _context.EmployeeContract
                .AsNoTracking()
                .Include(X => X.WorkHours)
                .FirstOrDefaultAsync(c => c.ID == entityId)
                    ?? throw new KeyNotFoundException(MessageRecords.KeyNotFounded);
        }

        public async Task UpdateAsync(EmployeeContract entity)
        {
            await _transaction.Execute(_context, async () =>
            {
                var employeeContract = await _context.EmployeeContract.AsNoTracking()
                .Where(x => x.ID == entity.ID)
                .FirstOrDefaultAsync()
                    ?? throw new KeyNotFoundException(MessageRecords.KeyNotFounded);
            });
        }
    }
}

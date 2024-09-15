using HMS.PayrollMicroService.Core.Entity;
using HMS.PayrollMicroService.Core.Repository;
using HMS.PayrollMicroService.Infratructure.Context;
using HMS.PayrollMicroService.Infratructure.Messages.Errors;
using HMS.PayrollMicroService.Infratructure.Services;
using Microsoft.EntityFrameworkCore;

namespace HMS.PayrollMicroService.Infratructure.Repository
{
    public sealed class PayrollRepository : IPayrollRepository
    {
        private readonly TransactionService _transaction;
        private readonly PayrollContext _context;
        public PayrollRepository(PayrollContext context, TransactionService transaction)
        {
            _transaction = transaction;
            _context = context;
        }
        public async Task AddAsync(Payroll payroll)
        {
            try
            {
                await _transaction.Execute(_context, async () =>
                {
                    await _context.Payroll.AddAsync(payroll);
                });
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task DeleteAsync(Guid payrollID)
        {
            try
            {
                var payroll = await _context.Payroll
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.ID == payrollID)
                        ?? throw new KeyNotFoundException(DefaultErrorMessages.PayrollNotFounded);
                await _transaction.Execute(_context, async () =>
                {
                    await Task.Run(() => _context.Payroll.Remove(payroll));
                });
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<Payroll>> GetAllAsync()
        {
            return await _context.Payroll
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<List<Payroll>> GetByEmployeeID(Guid employeeID)
        {
            try
            {
                return await _context.Payroll.AsNoTracking()
                    .Where(x => x.EmployeeId == employeeID)
                    .ToListAsync()
                        ?? throw new KeyNotFoundException(DefaultErrorMessages.PayrollNotFounded);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<Payroll> GetByID(Guid ID)
        {
            try
            {
                return await _context.Payroll
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.ID == ID)
                        ?? throw new KeyNotFoundException(DefaultErrorMessages.PayrollNotFounded);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task UpdateAsync(Payroll payroll)
        {
            try
            {
                await _transaction.Execute(_context, async () =>
                {
                    var currentPayroll = await _context.Payroll.FindAsync(payroll.ID)
                        ?? throw new KeyNotFoundException(DefaultErrorMessages.PayrollNotFounded);
                    await Task.Run(() => {
                        currentPayroll.Update(payroll);
                        _context.Payroll.Update(payroll);
                        });
                });
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}

using HMS.Payments.Core.Entity;
using HMS.Payments.Core.Repository;
using HMS.Payments.Infratructure.Context;
using HMS.Payments.Infratructure.Messages.Errors;
using HMS.Payments.Infratructure.Services;
using Microsoft.EntityFrameworkCore;

namespace HMS.Payments.Infratructure.Repository
{
    public sealed class EmployeePaymentRepository : IEmployeePaymentRepository
    {
        private readonly TransactionService _transaction;
        public EmployeePaymentRepository(EmployeePaymentContext context, TransactionService transaction)
        {
            _transaction = transaction;
            _context = context;
        }
        public async Task AddAsync(Core.Entity.EmployeePayment payroll)
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

        public async Task<List<Core.Entity.EmployeePayment>> GetAllAsync()
        {
            return await _context.Payroll
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<List<Core.Entity.EmployeePayment>> GetByEmployeeID(Guid employeeID)
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

        public async Task<Core.Entity.EmployeePayment> GetByID(Guid ID)
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

        public async Task UpdateAsync(Core.Entity.EmployeePayment payroll)
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

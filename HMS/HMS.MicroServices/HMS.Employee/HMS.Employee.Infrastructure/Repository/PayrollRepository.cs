using HMS.Employee.Core.Entity;
using HMS.Employee.Core.Interface.Repository;
using HMS.Employee.Core.Json;
using HMS.Employee.Infrastructure.DataContext;
using HMS.Employee.Infrastructure.MessageResponse;
using HMS.Employee.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Employee.Infrastructure.Repository
{
    public sealed class PayrollRepository : IRepositoryWithEmployeeId<Payroll>
    {
        private readonly PayrollContext _context;
        private readonly TransactionService _transaction;
        public PayrollRepository(PayrollContext context, TransactionService transaction)
        {
            _context = context;    
            _transaction = transaction;
        }
        public async Task<bool> Add(Payroll entity)
        {
            try
            {
                var teste = await JsonConvert.Serialize(entity);
                Console.WriteLine(teste);
                int rowsAffected = await _transaction.Execute(_context, async () =>
                {
                    await _context.Payroll.AddAsync(entity);
                });
                if (rowsAffected != 1)
                    throw new Exception(DefaultMessages.ErrorTransactionActive);
                return rowsAffected == 1;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Task<bool> Delete(Payroll ID)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Payroll>> Get()
        {
            try
            {
                return await _context.Payroll
                    .AsNoTracking()
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<Payroll>> GetByEmployeeId(Guid ID)
        {
            try
            {
                return await _context.Payroll
                    .AsNoTracking()
                    .Where(x => x.EmployeeId == ID)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<Payroll> GetById(Guid ID)
        {
            try
            {
                return await _context.Payroll
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == ID);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Task<bool> Update(Payroll entity)
        {
            throw new NotImplementedException();
        }
    }
}

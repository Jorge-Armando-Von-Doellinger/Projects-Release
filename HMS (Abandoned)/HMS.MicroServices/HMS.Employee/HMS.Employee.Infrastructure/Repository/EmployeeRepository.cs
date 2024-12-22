using HMS.Employee.Core.Entity;
using HMS.Employee.Core.Interface.Manager;
using HMS.Employee.Core.Interface.Repository;
using HMS.Employee.Core.Json;
using HMS.Employee.Infrastructure.Context;
using HMS.Employee.Infrastructure.DataContext;
using HMS.Employee.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

namespace HMS.Employee.Infrastructure.Repository
{
    public sealed class EmployeeRepository : IRepository<Core.Entity.Employee>
    {
        private readonly DefaultContext _context;  
        private readonly TransactionService _transaction;
        public EmployeeRepository(DefaultContext context, TransactionService transaction)
        {
            _context = context;
            _transaction = transaction;
        }

        public async Task<bool> Add(Core.Entity.Employee entity)
        {
            try
            {
                var rowsAffected = await _transaction.Execute(_context, async () =>
                {
                    await _context.Employee.AddAsync(entity);
                });
                return rowsAffected == 1;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> Delete(Guid ID)
        {
            try
            {
                var rowsAffected = await _transaction.Execute(_context, async () =>
                {
                    var employee = await _context.Employee.FindAsync(ID)
                        ?? throw new NullReferenceException("Nenhum contrato encontrado!");
                    await Task.Run(() => _context.Employee.Remove(employee));
                });
                return rowsAffected == 1;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<Core.Entity.Employee>> Get()
        {
            try
            {
                return await _context.Employee
                    .AsNoTracking()
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<Core.Entity.Employee> GetById(Guid ID)
        {
            try
            {
                return await _context.Employee
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == ID);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> Update(Core.Entity.Employee entity)
        {
            try
            {
                var rowsAffected = await _transaction.Execute(_context, async () =>
                {
                    var employee = await _context.Employee.FindAsync(entity.Id);
                    //await employee.Update(entity);
                });
                return rowsAffected == 1;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}

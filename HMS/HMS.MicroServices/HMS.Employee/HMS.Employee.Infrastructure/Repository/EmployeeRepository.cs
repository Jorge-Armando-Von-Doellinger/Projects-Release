using HMS.Employee.Core.Entity;
using HMS.Employee.Core.Interface.Manager;
using HMS.Employee.Core.Interface.Repository;
using HMS.Employee.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace HMS.Employee.Infrastructure.Repository
{
    public sealed class EmployeeRepository : IRepository<Core.Entity.Employee>
    {
        private readonly EmployeeContext _context;  
        public EmployeeRepository(EmployeeContext context)
        {
            _context = context;
        }

        public async Task<Core.Entity.Employee> Add(Core.Entity.Employee entity)
        {
            try
            {
                await _context.Employee.AddAsync(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Task<Core.Entity.Employee> Delete(Core.Entity.Employee entity)
        {
            throw new NotImplementedException();
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

        public Task<Core.Entity.Employee> GetById(Guid ID)
        {
            throw new NotImplementedException();
        }

        public Task<Core.Entity.Employee> Update(Core.Entity.Employee entity)
        {
            throw new NotImplementedException();
        }
    }
}

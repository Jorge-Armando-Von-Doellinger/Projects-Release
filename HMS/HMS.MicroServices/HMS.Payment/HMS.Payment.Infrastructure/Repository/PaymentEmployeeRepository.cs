using HMS.Payments.Core.Entity;
using HMS.Payments.Core.Interfaces.Repository;
using HMS.Payments.Infrastructure.Context;
using HMS.Payments.Infratructure.Services;
using MongoDB.Driver;

namespace HMS.Payments.Infrastructure.Repository
{
    
    public sealed class PaymentEmployeeRepository : IPaymentEmployeeRepository
    {
        private readonly MongoContext _context;
        private readonly TransactionService _transaction;
        
        public PaymentEmployeeRepository(MongoContext context, TransactionService transaction)
        {
            _context = context;
            _transaction = transaction;
        }

        public async Task AddManyAsync(List<PaymentEmployee> employees)
        {
            if(employees?.Count <= 0) return;
            await _context.PaymentEmployee.InsertManyAsync(employees);
        }

        public async Task AddAsync(PaymentEmployee payment)
        {
            await _context.PaymentEmployee.InsertOneAsync(payment);
        }

        public async Task<PaymentEmployee> GetByIdAsync(string id)
        {
            var docs = await _context.PaymentEmployee.FindAsync(x => x.ID == id);
            return await docs.FirstOrDefaultAsync();
        }

        public async Task<List<PaymentEmployee>> GetAllAsync()
        {
            var docs = await _context.PaymentEmployee.FindAsync(x => true);
            return await docs.ToListAsync();
        }

        public async Task<List<PaymentEmployee>> GetByEmployeeId(string employeeId)
        {
            var docs = await _context.PaymentEmployee.FindAsync(x => x.EmployeeId == employeeId);
            return await docs.ToListAsync();
        }
    }
}

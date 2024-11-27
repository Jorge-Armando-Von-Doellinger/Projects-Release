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
        public async Task AddAsync(PaymentEmployee payment)
        {
            await _transaction.ExecuteAsync(async (session) =>
            {
                await _context.Payment.InsertOneAsync(session, payment);
            });
        }

        public Task DeleteAsync(string paymentId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<PaymentEmployee>> GetAllAsync()
        {
            var documents = await _context.PaymentEmployee.FindAsync(doc => true);
            return await documents.ToListAsync();
        }

        public async Task<List<PaymentEmployee>> GetByEmployeeID(string employeeId)
        { ;
            var documents = await _context.PaymentEmployee.FindAsync(x => x.EmployeeId == employeeId);
            return await documents.ToListAsync();
        }

        public async Task<PaymentEmployee> GetByIDAsync(string id)
        {
            var document = await _context.PaymentEmployee.FindAsync(doc => doc.ID == id);
            return await document.FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(PaymentEmployee payment)
        {
            await _transaction.ExecuteAsync(async (session) =>
            {
                await _context.PaymentEmployee.ReplaceOneAsync(session, doc => doc.ID == payment.ID, payment);
            });
        }
    }
}

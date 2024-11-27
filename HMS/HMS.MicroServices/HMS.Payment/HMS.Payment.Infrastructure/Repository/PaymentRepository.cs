using HMS.Payments.Core.Entity;
using HMS.Payments.Core.Interfaces.Repository;
using HMS.Payments.Infrastructure.Context;
using HMS.Payments.Infratructure.Services;
using MongoDB.Driver;

namespace HMS.Payments.Infrastructure.Repository
{
    public sealed class PaymentRepository : IPaymentRepository
    {
        private readonly MongoContext _context;
        private readonly TransactionService _transaction;

        public PaymentRepository(MongoContext context, TransactionService transaction)
        {
            _context = context;
            _transaction = transaction;
        }
        public async Task AddAsync(Payment payment)
        {
            Console.WriteLine(payment.BeneficiaryCPF);
            await _transaction.ExecuteAsync(async (session) =>
            {
                await _context.Payment.InsertOneAsync(session, payment);
            });
        }

        public async Task AddBatchAsync(IEnumerable<Payment> payments)
        {
            await _context.Payment.InsertManyAsync(payments);
        }

        public Task UpdateBatchAsync(IEnumerable<Payment> payments)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Payment payment)
        {
            throw new NotImplementedException();
        }

        public Task DeleteBatchAsync(IEnumerable<string> payments)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Payment>> GetAllAsync()
        {
            var documents = await _context.Payment.FindAsync(p => true);
            return await documents.ToListAsync();
        }

        public async Task<Payment> GetByIdAsync(string id)
        {
            var document = await _context.Payment.FindAsync(doc => doc.ID == id);
            return await document.FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(Payment payment)
        {
            await _transaction.ExecuteAsync(async (session) =>
            {
                await _context.Payment.ReplaceOneAsync(session, x => x.ID == payment.ID, payment);
            });
        }
    }
}

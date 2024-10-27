using HMS.Payments.Core.Entity;
using HMS.Payments.Core.Interfaces.Repository;
using HMS.Payments.Infrastructure.Connect;
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
        public async Task AddPayment(Payment payment)
        {
            await _transaction.Execute(async (session) =>
            {
                await _context.Payment.InsertOneAsync(session, payment);
            });
        }

        public Task DeletePayment(Payment payment)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Payment>> GetAll()
        {
            var documents = await _context.Payment.FindAsync(p => true);
            return await documents.ToListAsync();
        }

        public async Task<Payment> GetPaymentById(string id)
        {
            var document = await _context.Payment.FindAsync(doc => doc.ID == id);
            return await document.FirstOrDefaultAsync();
        }

        public async Task UpdatePayment(Payment payment)
        {
            await _transaction.Execute(async (session) =>
            {
                await _context.Payment.ReplaceOneAsync(session, x => x.ID == payment.ID, payment);
            });
        }
    }
}

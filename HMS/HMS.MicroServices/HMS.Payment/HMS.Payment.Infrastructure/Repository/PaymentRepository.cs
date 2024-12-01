using HMS.Payments.Core.Entity;
using HMS.Payments.Core.Enums;
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
        private FilterDefinition<Payment> GetFilter(string id) => Builders<Payment>.Filter.Eq(p => p.ID, id);
        private UpdateDefinition<Payment> GetUpdateDefinition(PaymentStatus status) => Builders<Payment>.Update
            .Set(p => p.Status, status);

        public async Task AddManyAsync(List<Payment> payments)
        {
            if (payments.Count == 0) return;
            await _transaction.ExecuteAsync(async (session) =>
            {
                await _context.Payment.InsertManyAsync(session, payments);
            });
        }

        public async Task AddAsync(Payment payment)
        {
            await _transaction.ExecuteAsync(async (session) =>
            {
                await _context.Payment.InsertOneAsync(payment);
            });
        }

        public async Task AddRefundAsync(Payment payment)
        {
            var filter = GetFilter(payment.ID);
            var update = GetUpdateDefinition(payment.Status);
            await _context.Payment.UpdateOneAsync(filter, update);
        }

        public async Task<Payment> GetByIdAsync(string id)
        {
            var docs = await _context.Payment.FindAsync(p => p.ID == id);
            return await docs.FirstOrDefaultAsync();
        }

        public async Task<List<Payment>> GetAllAsync()
        {
            var docs = await _context.Payment.FindAsync(p => true);
            return await docs.ToListAsync();
        }
    }
}

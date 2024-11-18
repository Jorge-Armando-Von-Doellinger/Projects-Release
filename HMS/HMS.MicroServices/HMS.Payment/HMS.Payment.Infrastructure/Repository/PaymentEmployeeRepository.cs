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
            _context.AddOperation(new InsertOneModel<PaymentEmployee>(payment));
            /*            payment.ID = null;
                        await _transaction.Execute(async (session) =>
                        {
                            await _context.PaymentEmployee.InsertOneAsync(session, payment);
                        });*/
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

        public async Task<List<PaymentEmployee>> GetByEmployeeID(string EmployeeID)
        {
            var documents = await _context.PaymentEmployee.FindAsync(x => x.EmployeeId == EmployeeID);
            return await documents.ToListAsync();
        }

        public async Task<PaymentEmployee> GetByIDAsync(string ID)
        {
            var document = await _context.PaymentEmployee.FindAsync(doc => doc.ID == ID);
            return await document.FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(PaymentEmployee payment)
        {
            await _transaction.Execute(async (session) =>
            {
                await _context.PaymentEmployee.ReplaceOneAsync(session, doc => doc.ID == payment.ID, payment);
            });
        }
    }
}

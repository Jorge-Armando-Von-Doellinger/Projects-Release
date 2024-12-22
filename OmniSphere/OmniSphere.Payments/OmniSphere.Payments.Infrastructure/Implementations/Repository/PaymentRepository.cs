using MongoDB.Driver;
using OmniSphere.Payments.Core.Entity;
using OmniSphere.Payments.Core.Interfaces.Repository;

namespace OmniSphere.Payments.Infrastructure.Implementations.Repository;

public class PaymentRepository : IPaymentRepository
{
    private readonly IMongoCollection<Payment> _collection;

    public PaymentRepository(IMongoCollection<Payment> collection)
    {
        _collection = collection;
    }
    public async Task RegisterAsync(Payment payment)
    {
        await _collection.InsertOneAsync(payment);
    }

    public async Task DeleteAsync(Payment payment)
    {
        var def = Builders<Payment>.Update.Set(x => x.IsDeleted, true);
        await _collection.UpdateOneAsync(x => x.Status == payment.Status, def);
    }

    public async Task<Payment> GetByIdAsync(string paymentId)
    {
        var doc = await _collection.FindAsync(x => x.Id == paymentId);
        var payment = await doc.FirstOrDefaultAsync();
        return payment;
    }

    public async Task<List<Payment>> GetByAccountIdAsync(string accountId)
    {
        var docs = await _collection.FindAsync(x => x.Id == accountId);
        var payments = await docs.ToListAsync();
        return payments;
    }
}
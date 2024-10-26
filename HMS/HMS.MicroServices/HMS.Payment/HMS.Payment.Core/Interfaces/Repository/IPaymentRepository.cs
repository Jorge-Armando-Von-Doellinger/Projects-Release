using HMS.Payments.Core.Entity;

namespace HMS.Payments.Core.Interfaces.Repository
{
    public interface IPaymentRepository // Ela nãó realiza pagamentos, somente cadastra os já realizados!
    {
        Task AddPayment(Payment payment);
        Task UpdatePayment(Payment payment);
        Task DeletePayment(Payment payment);
        Task<Payment> GetPaymentById(string id);
        Task<List<Payment>> GetAll();
    }
}

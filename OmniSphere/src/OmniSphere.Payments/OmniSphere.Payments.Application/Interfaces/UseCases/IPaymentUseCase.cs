using OmniSphere.Payments.Application.DTOs;

namespace OmniSphere.Payments.Application.Interfaces.UseCases;

public interface IPaymentUseCase
{
    Task AddPaymentAsync(PaymentDTO payment);
    Task DeletePaymentAsync(string paymentId, string accountId);
    Task<PaymentWithStatusAndIdDTO> GetPaymentByIdAsync(string paymentId);
    Task<IEnumerable<PaymentWithStatusAndIdDTO>> GetPaymentsByAccountIdAsync(string accountId);
}
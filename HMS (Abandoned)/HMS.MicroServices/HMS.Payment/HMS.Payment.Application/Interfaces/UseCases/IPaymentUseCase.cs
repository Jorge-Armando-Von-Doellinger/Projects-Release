using HMS.Payments.Application.Models.Input;
using HMS.Payments.Application.Models.Output;

namespace HMS.Payments.Application.Interfaces.UseCases;

public interface IPaymentUseCase
{
    Task AddAsync(PaymentModel input);
    Task AddBatchAsync(IEnumerable<PaymentModel> inputs);
    Task<List<PaymentOutput>> GetAllAsync();
    Task<PaymentOutput> GetByIdAsync(string id);
}
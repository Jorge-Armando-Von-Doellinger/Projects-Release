using HMS.Payments.Application.Models.Input;
using HMS.Payments.Application.Models.Output;
using HMS.Payments.Application.Models.Update;

namespace HMS.Payments.Application.Interfaces.Manager
{
    public interface IPaymentManager
    {
        Task AddAsync(PaymentModel input);
        Task UpdateAsync(PaymentUpdateModel input); // Update somente nas razões, para manter a integridade!
        // Task Delete(PaymentModel input); Não haverão deletes de pagameento, isso para manter a integridade!
        Task<List<PaymentOutput>> GetAllAsync();
        Task<PaymentOutput> GetByIdAsync(string id);
        // Task GetByData(); Futuramente ainda
    }
}

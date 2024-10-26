using HMS.Payments.Application.Extensions;
using HMS.Payments.Application.Interfaces.Manager;
using HMS.Payments.Application.Models.Input;
using HMS.Payments.Application.Models.Update;
using HMS.Payments.Core.Entity;
using HMS.Payments.Core.Interfaces.Repository;
using System.Text.Json;

namespace HMS.Payments.Application.Manager
{
    public sealed class PaymentManager : IPaymentManager
    {
        private readonly IPaymentRepository _repository;

        public PaymentManager(IPaymentRepository repository)
        {
            _repository = repository;
        }
        public async Task AddAsync(PaymentModel input)
        {
            var mapper = LambdaMapper.CreateDynamicMapper<PaymentModel, Payment>();
            var entity = mapper(input);
            var serialized = JsonSerializer.Serialize(entity);
            Console.WriteLine(serialized);
            await _repository.AddPayment(entity);
        }

        public async Task<List<Payment>> GetAll()
        {
            return await _repository.GetAll();
        }

        public Task<Payment> GetByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(PaymentUpdateModel input)
        {
            throw new NotImplementedException();
        }
    }
}

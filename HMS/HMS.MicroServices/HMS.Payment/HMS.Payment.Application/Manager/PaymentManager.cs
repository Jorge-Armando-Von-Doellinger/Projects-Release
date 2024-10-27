using HMS.Payments.Application.Interfaces.Manager;
using HMS.Payments.Application.Mapper;
using HMS.Payments.Application.Models.Input;
using HMS.Payments.Application.Models.Output;
using HMS.Payments.Application.Models.Update;
using HMS.Payments.Core.Interfaces.Repository;

namespace HMS.Payments.Application.Manager
{
    public sealed class PaymentManager : IPaymentManager
    {
        private readonly IPaymentRepository _repository;
        private readonly PaymentMapper _mapper;

        public PaymentManager(IPaymentRepository repository, PaymentMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task AddAsync(PaymentModel input)
        {
            var entity = _mapper.Map(input);
            entity.ValidateEntity();
            await _repository.AddPayment(entity);
        }

        public async Task<List<PaymentOutput>> GetAll()
        {
            var payments = await _repository.GetAll();
            var output = _mapper.Map(payments);
            return output;
        }

        public async Task<PaymentOutput> GetByIdAsync(string id)
        {
            var payment = await _repository.GetPaymentById(id);
            var output = _mapper.Map(payment);

            return output;
        }

        public async Task UpdateAsync(PaymentUpdateModel input)
        {
            var entity = _mapper.Map(input);
            entity.ValidateEntity();
            await _repository.UpdatePayment(entity);
        }
    }
}

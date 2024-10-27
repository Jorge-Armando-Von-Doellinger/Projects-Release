using HMS.Payments.Application.Interfaces.Manager;
using HMS.Payments.Application.Mapper;
using HMS.Payments.Application.Models.Input;
using HMS.Payments.Application.Models.Output;
using HMS.Payments.Application.Models.Update;
using HMS.Payments.Core.Exceptions;
using HMS.Payments.Core.Interfaces.Repository;
using HMS.Payments.Core.Interfaces.Services;

namespace HMS.Payments.Application.Manager
{
    public sealed class EmployeePaymentManager : IEmployeePaymentManager
    {
        private readonly IEmployeePaymentRepository _repository;
        private readonly EmployeePaymentMapper _mapper;
        private readonly IPaymentService _paymentService;
        public EmployeePaymentManager(IEmployeePaymentRepository repository, EmployeePaymentMapper mapper, IPaymentService paymentService)
        {
            _repository = repository;
            _mapper = mapper;
            _paymentService = paymentService;
        }

        public async Task AddAsync(PaymentEmployeeModel input)
        {
            var entity = _mapper.Map(input);
            entity.ValidateEntity();
            var success = await _paymentService.TryProcessPayment(entity);
            await _repository.AddAsync(entity);
            if (!success) throw new PaymentInvalidException("Não foi possivel realizar o pagamento!");
        }

        public async Task DeleteAsync(string ID)
        {
            await _repository.DeleteAsync(ID);
        }

        public async Task<List<PaymentEmployeeOutput>> GetAsync()
        {
            var payments = await _repository.GetAllAsync();
            var output = _mapper.Map(payments);
            return output;
        }

        public async Task<List<PaymentEmployeeOutput>> GetByEmployeeIdAsync(string employeeID)
        {
            var payments = await _repository.GetByEmployeeID(employeeID);
            var output = _mapper.Map(payments);
            return output;
        }

        public async Task<PaymentEmployeeOutput> GetByIdAsync(string ID)
        {
            var payment = await _repository.GetByIDAsync(ID);
            var output = _mapper.Map(payment);
            return output;
        }

        public async Task UpdateAsync(PaymentEmployeeUpdateModel input)
        {
            var entity = _mapper.Map(input);
            entity.ValidateEntity();
            await _repository.UpdateAsync(entity);
        }
    }
}

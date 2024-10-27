using HMS.Payments.Application.Interfaces.Manager;
using HMS.Payments.Application.Mapper;
using HMS.Payments.Application.Models.Input;
using HMS.Payments.Application.Models.Output;
using HMS.Payments.Application.Models.Update;
using HMS.Payments.Core.Interfaces.Repository;

namespace HMS.Payments.Application.Manager
{
    public sealed class EmployeePaymentManager : IEmployeePaymentManager
    {
        private readonly IEmployeePaymentRepository _repository;
        private readonly EmployeePaymentMapper _mapper;

        public EmployeePaymentManager(IEmployeePaymentRepository repository, EmployeePaymentMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task AddAsync(PaymentEmployeeModel input)
        {
            var entity = _mapper.Map(input);
            await _repository.AddAsync(entity);
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

        public Task<List<PaymentEmployeeOutput>> GetByEmployeeIdAsync(string employeeID)
        {
            throw new NotImplementedException();
        }

        public async Task<PaymentEmployeeOutput> GetByIdAsync(string ID)
        {
            var payment = await _repository.GetByID(ID);
            var output = _mapper.Map(payment);
            return output;
        }

        public async Task UpdateAsync(PaymentEmployeeUpdateModel input)
        {
            var entity = _mapper.Map(input);
            await _repository.UpdateAsync(entity);
        }
    }
}

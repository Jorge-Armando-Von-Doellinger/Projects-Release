using HMS.Payments.Application.Extensions;
using HMS.Payments.Application.Interfaces.Manager;
using HMS.Payments.Application.Models.Input;
using HMS.Payments.Application.Models.Output;
using HMS.Payments.Core.Entity;
using HMS.Payments.Core.Interfaces.Repository;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace HMS.Payments.Application.Manager
{
    public sealed class EmployeePaymentManager : IEmployeePaymentManager
    {
        private readonly IEmployeePaymentRepository _repository;
        public EmployeePaymentManager(IEmployeePaymentRepository repository)
        {
            _repository = repository;
        }

        public async Task AddAsync(EmployeePaymentModel input)
        {
            var mapper = LambdaMapper.CreateDynamicMapper<EmployeePaymentModel, PaymentEmployee>();
            var entity = mapper(input);
            var data = JsonSerializer.Serialize(entity);
            Console.WriteLine(data);
            await _repository.AddAsync(entity);
        }

        public Task DeleteAsync(Guid ID)
        {
            throw new NotImplementedException();
        }

        public async Task<List<PaymentEmployee>> GetAsync()
        {
            return await _repository.GetAllAsync();
        }

        public Task<List<EmployeePaymentOutput>> GetByEmployeeIdAsync([MaxLength(100)] string employeeID)
        {
            throw new NotImplementedException();
        }

        public Task<EmployeePaymentOutput> GetByIdAsync(string ID)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(EmployeePaymentModel input)
        {
            throw new NotImplementedException();
        }
    }
}

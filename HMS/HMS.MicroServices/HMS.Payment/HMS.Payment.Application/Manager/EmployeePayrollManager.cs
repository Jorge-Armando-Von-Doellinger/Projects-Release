using HMS.Payments.Application.Interfaces.Manager;
using HMS.Payments.Core.Entity;
using HMS.Payments.Core.Json;
using HMS.Payments.Core.Mapper;
using HMS.Payments.Core.Repository;
using Nuget.Payment.Input;

namespace HMS.Payments.Application.Manager
{
    public sealed class PaymentManager : IEmployeePayrollManager
    {
        private readonly IEmployeePaymentRepository _payrollRepository;

        public PaymentManager(IEmployeePaymentRepository payrollRepository) 
        { 
            _payrollRepository = payrollRepository;
        }

        public async Task<bool> AddAsync(PaymentInput input)
        {
            try
            {
                var payroll = input.FromObjectTo<EmployeePayment>();
                Console.WriteLine(await JsonConvert.Serialize(payroll));
                await _payrollRepository.AddAsync(payroll);
                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task DeleteAsync(Guid ID)
        {
            try
            {
                await _payrollRepository.DeleteAsync(ID);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<EmployeePayment>> GetAsync()
        {
            try
            {
                return await _payrollRepository.GetAllAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<EmployeePayment>> GetByEmployeeIdAsync(Guid employeeID)
        {
            try
            {
                return await _payrollRepository.GetByEmployeeID(employeeID);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<EmployeePayment> GetByIdAsync(Guid ID)
        {
            try
            {
                return await _payrollRepository.GetByID(ID);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task UpdateAsync(PaymentInput input)
        {
            try
            {
                var payroll = 
                    input.FromObjectTo<EmployeePayment>()
                    ?? throw new InvalidDataException("Valores de entrada invalidos!");
                await _payrollRepository.UpdateAsync(payroll);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}

using HMS.PayrollMicroService.Application.Interfaces.Manager;
using HMS.PayrollMicroService.Core.Entity;
using HMS.PayrollMicroService.Core.Json;
using HMS.PayrollMicroService.Core.Mapper;
using HMS.PayrollMicroService.Core.Repository;
using Nuget.Payroll.Input;

namespace HMS.PayrollMicroService.Application.Manager
{
    public sealed class PayrollManager : IPayrollManager
    {
        private readonly IPayrollRepository _payrollRepository;

        public PayrollManager(IPayrollRepository payrollRepository) 
        { 
            _payrollRepository = payrollRepository;
        }

        public async Task<bool> AddAsync(PayrollInput input)
        {
            try
            {
                var payroll = input.FromObjectTo<Payroll>();
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

        public async Task<List<Payroll>> GetAsync()
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

        public async Task<List<Payroll>> GetByEmployeeIdAsync(Guid employeeID)
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

        public async Task<Payroll> GetByIdAsync(Guid ID)
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

        public async Task UpdateAsync(PayrollInput input)
        {
            try
            {
                var payroll = 
                    input.FromObjectTo<Payroll>()
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

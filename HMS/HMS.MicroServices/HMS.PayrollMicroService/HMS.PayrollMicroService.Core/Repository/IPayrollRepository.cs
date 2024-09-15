using HMS.PayrollMicroService.Core.Entity;

namespace HMS.PayrollMicroService.Core.Repository
{
    public interface IPayrollRepository
    {
        Task AddAsync(Payroll payroll);
        Task UpdateAsync(Payroll payroll);
        Task DeleteAsync(Guid payrollID);
        Task<Payroll> GetByID(Guid ID);
        Task<List<Payroll>> GetAllAsync();
        Task<List<Payroll>> GetByEmployeeID(Guid EmployeeID);
    }
}

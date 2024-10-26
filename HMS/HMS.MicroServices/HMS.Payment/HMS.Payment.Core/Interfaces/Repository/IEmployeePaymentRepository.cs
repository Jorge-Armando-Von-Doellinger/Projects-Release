using HMS.Payments.Core.Entity;

namespace HMS.Payments.Core.Interfaces.Repository
{
    public interface IEmployeePaymentRepository
    {
        Task AddAsync(EmployeePayment payroll);
        Task UpdateAsync(EmployeePayment payroll);
        Task DeleteAsync(Guid payrollID);
        Task<EmployeePayment> GetByID(Guid ID);
        Task<List<EmployeePayment>> GetAllAsync();
        Task<List<EmployeePayment>> GetByEmployeeID(Guid EmployeeID);
    }
}

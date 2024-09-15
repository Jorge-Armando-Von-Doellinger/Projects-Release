using HMS.Payments.Core.Entity;

namespace HMS.Payments.Core.Repository
{
    public interface IEmployeePaymentRepository
    {
        Task AddAsync(Entity.EmployeePayment payroll);
        Task UpdateAsync(Entity.EmployeePayment payroll);
        Task DeleteAsync(Guid payrollID);
        Task<Entity.EmployeePayment> GetByID(Guid ID);
        Task<List<Entity.EmployeePayment>> GetAllAsync();
        Task<List<Entity.EmployeePayment>> GetByEmployeeID(Guid EmployeeID);
    }
}

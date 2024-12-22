using HMS.Employee.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace HMS.Employee.Infrastructure.DataContext.ConfiguringContexts
{
    internal class ContextConfigurator
    {
        internal void ConfigureAllContexts(ModelBuilder modelBuilder)
        {
            var contractualConfiguring = new ContractualConfiguring(modelBuilder);
            var employeeConfiguring = new EmployeeConfiguring(modelBuilder);
            var payrollConfiguring = new PayrollConfiguring(modelBuilder);

        }
    }
}

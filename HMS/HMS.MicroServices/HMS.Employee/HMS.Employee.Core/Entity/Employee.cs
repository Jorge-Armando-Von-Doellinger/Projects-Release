using HMS.Employee.Core.Entity.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HMS.Employee.Core.Entity
{
    public sealed class Employee : BaseEntity
    {
        // OBS: Optei por deixar cada micro servico isolado de outros banco de dados :)
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public long CPF { get; set; }
        public long PIS { get; set; }
        public Int16 Age { get; set; }
        public DateOnly BirthDate { get; set; }
        public string MaritalStatus { get; set; } // Enum

        //Contractual
        public Guid ContractId { get; set; }
        public ContractualInformation ContractualInformation { get; set; }

        // Payrolls
        //public Payroll Payroll { get; set; }

        public async Task Update(Employee UpdatedEmployee)
        {
            await Task.Run(() =>
            {
                base.Update();
                Name = UpdatedEmployee.Name ?? Name;
                Email = UpdatedEmployee.Email ?? Email;
                PhoneNumber = UpdatedEmployee.PhoneNumber ?? PhoneNumber;
                CPF = UpdatedEmployee?.CPF ?? CPF;
                PIS = UpdatedEmployee?.PIS ?? PIS;
                Age = UpdatedEmployee?.Age ?? Age;
                BirthDate = UpdatedEmployee?.BirthDate ?? BirthDate;
                MaritalStatus = UpdatedEmployee?.MaritalStatus ?? MaritalStatus;
            });
        }

    }
}

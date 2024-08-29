using HMS.Employee.Core.Entity.Base;
using HMS.Employee.Core.Enum;

namespace HMS.Employee.Core.Entity
{
    public sealed class ContractualInformation : BaseEntityWithEmployeeId
    {
        // Irei evitar o uso de banco de dados nesta parte. Se tratando de um micro-serviço pequeno, isso vai bastar!
        public DateTime StartDate { get; set; }
        public JobTitleEnum JobTitle { get; set; } // Enum ou banco de dados dedicado
        public ExperienceLevel ExperienceLevel { get; set; }
        public DepartmentEnum Department { get; set; } //Enum ou um banco de dados dedicado
        public short HourlySalaryInDollar { get; set; } // Praticamente ninguem recebe +32k/hora
        public List<BenefitsEnum> Benefits { get; set; } // Enum ou banco de dados dedicado
        public List<TimeOnly> WorkingHours { get; set; } // Ex: 08:00, 9:00, 11:00
        public TimeOnly LunchTime { get; set; }
        public ContractTypeEnum ContractType { get; set; } // Enum
        public short ProbationPeriodInMonths { get; set; }
        public EmploymentStatusEnum EmploymentStatus { get; set; } // Enum
        public DateOnly EndDay { get; set; }
    }
}

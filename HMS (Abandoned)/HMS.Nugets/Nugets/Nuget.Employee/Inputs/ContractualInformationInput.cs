using Nuget.Employee.Enum;
using Nuget.Employee.Inputs.Base;
using System.Text.Json.Serialization;

namespace Nuget.Employee.Inputs
{
    public class ContractualInformationInput<Benefit> : BaseWithEmployeeId
    {
        public DateTime StartDate { get; set; }
        public JobTitleEnum JobTitle { get; set; } // Enum ou banco de dados dedicado
        public ExperienceLevel ExperienceLevel { get; set; }
        public DepartmentEnum Department { get; set; } //Enum ou um banco de dados dedicado
        public short HourlySalaryInDollar { get; set; } // Praticamente ninguem recebe +32k/hora
        public List<Benefit> Benefits { get; set; } // Enum ou banco de dados dedicado
        public List<TimeOnly> WorkingHours { get; set; } // Ex: 08:00 - Começo; 12:00 - Almoço; 16:00 - Fim
        public ContractTypeEnum ContractType { get; set; } // Enum
        public short ProbationPeriodInMonths { get; set; }
        public EmploymentStatusEnum EmploymentStatus { get; set; } // Enum
        public DateOnly EndDay { get; set; }
    }
}

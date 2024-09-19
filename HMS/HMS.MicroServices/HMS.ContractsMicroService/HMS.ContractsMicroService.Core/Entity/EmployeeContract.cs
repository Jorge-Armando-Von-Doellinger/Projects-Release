using HMS.ContractsMicroService.Core.Entity.Base;
using HMS.ContractsMicroService.Core.Enums;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace HMS.ContractsMicroService.Core.Entity
{
    public sealed class EmployeeContract : EntityBaseWithId
    {
        public DateTime StartDate { get; set; }
        public JobTitleEnum JobTitle { get; set; } // Simucação de um banco de dados dedicado
        public ExperienceLevel ExperienceLevel { get; set; }
        public List<BenefitsEnum> Benefits { get; set; } // Simulacão de beneficios que podem estar armazenados em um banco de dados
        public DepartmentEnum Department { get; set; } //Enum ou um banco de dados dedicado
        public short HourlySalaryInDollar { get; set; } // Praticamente ninguem recebe +32k/hora
        public WorkHours WorkHours { get; set; } // Ex: 08:00-Inicio, 11:00-Almoco, 16h-Saida ; Seria melhor criar um banco de dados, talvez
        [Required]
        public Guid? WorkHoursID { get; set; } 
        public ContractTypeEnum ContractType { get; set; } // Enum
        public short ProbationPeriodInMonths { get; set; }
        public EmploymentStatusEnum EmploymentStatus { get; set; } // Enum
        public DateOnly EndDay { get; set; }
    }
}

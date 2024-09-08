using HMS.Employee.Core.Entity.Base;
using HMS.Employee.Core.Enum;
using HMS.Employee.Core.Mapper;
using System.Runtime.CompilerServices;

namespace HMS.Employee.Core.Entity
{
    public sealed class ContractualInformation : BaseEntityWithEmployeeId
    {
        // Irei evitar o uso de banco de dados nesta parte. Se tratando de um micro-serviço pequeno, isso vai bastar!
        public DateTime StartDate { get; set; }
        public JobTitleEnum JobTitle { get; set; } // Simucação de um banco de dados dedicado
        public ExperienceLevel ExperienceLevel { get; set; }
        public List<BenefitsEnum> Benefits { get; set; } // Simulacão de beneficios que podem estar armazenados em um banco de dados
        public DepartmentEnum Department { get; set; } //Enum ou um banco de dados dedicado
        public short HourlySalaryInDollar { get; set; } // Praticamente ninguem recebe +32k/hora
        public List<TimeOnly> WorkingHours { get; set; } // Ex: 08:00-Inicio, 11:00-Almoco, 16h-Saida
        public TimeOnly LunchTime { get; set; }
        public ContractTypeEnum ContractType { get; set; } // Enum
        public short ProbationPeriodInMonths { get; set; }
        public EmploymentStatusEnum EmploymentStatus { get; set; } // Enum
        public DateOnly EndDay { get; set; }

        public async Task UpdateAsync(ContractualInformation contractUpdated)
        {
            await Task.Run(() =>
            {
                this.Replacer(contractUpdated);
                base.Update();
            });
        }
    }
}

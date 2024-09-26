using System.Threading.Channels;

namespace Nuget.Contracts.Inputs
{
    public class EmployeeContractInput
    {
        public EmployeeContractInput()
        {
            JobTitle = -1;
            ExperienceLevel = -1;
            Department = -1;
            HourlySalaryInDollar = -1;
            ContractType = -1;
            ProbationPeriodInMonths = -1;
            EmploymentStatus = -1;
        }
        public DateTime StartDate { get; set; }
        public short JobTitle { get; set; }// Simucação de um banco de dados dedicado
        public sbyte ExperienceLevel { get; set; } 
        public List<short> Benefits { get; set; } = new (); // Simulacão de beneficios que podem estar armazenados em um banco de dados
        public short Department { get; set; } //Enum ou um banco de dados dedicado
        public short HourlySalaryInDollar { get; set; } // Praticamente ninguem recebe +32k/hora
        
        public Guid WorkHoursID { get; set; } 
        public short ContractType { get; set; }// Enum
        public sbyte ProbationPeriodInMonths { get; set; }
        public short EmploymentStatus { get; set; } // Enum
        public DateOnly EndDay { get; set; }
    }
}

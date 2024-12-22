namespace Nuget.Contracts.Outputs
{
    public sealed class EmployeeContractOutput
    {
        public EmployeeContractOutput()
        {
            JobTitle = string.Empty;
            ExperienceLevel = string.Empty;
            Benefits = string.Empty;
            Department = string.Empty;
            ContractType = string.Empty;
            EmploymentStatus = string.Empty;
        }
        public string ID { get; set; }
        public WorkHoursOutput WorkHours { get; set; }
        public DateTime StartDate { get; set; }
        public string JobTitle { get; set; }// Simucação de um banco de dados dedicado
        public string ExperienceLevel { get; set; }
        public string Benefits { get; set; } // Simulacão de beneficios que podem estar armazenados em um banco de dados
        public string Department { get; set; } //Enum ou um banco de dados dedicado
        public short HourlySalaryInDollar { get; set; } // Praticamente ninguem recebe +32k/hora
        public string ContractType { get; set; }// Enum
        public sbyte ProbationPeriodInMonths { get; set; }
        public string EmploymentStatus { get; set; } // Enum
        public DateOnly EndDay { get; set; }
    }
}

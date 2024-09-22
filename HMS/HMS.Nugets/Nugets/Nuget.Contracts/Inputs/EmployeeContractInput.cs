namespace Nuget.Contracts.Inputs
{
    public sealed class EmployeeContractInput
    {
        public DateTime StartDate { get; set; }
        public int JobTitle { get; set; } // Simucação de um banco de dados dedicado
        public short ExperienceLevel { get; set; }
        public List<int> Benefits { get; set; } = new(); // Simulacão de beneficios que podem estar armazenados em um banco de dados
        public int Department { get; set; } //Enum ou um banco de dados dedicado
        public short HourlySalaryInDollar { get; set; } // Praticamente ninguem recebe +32k/hora
        //public WorkHoursInput WorkHours { get; set; } = null;
        public Guid WorkHoursID { get; set; } // API irá preencher automaticamente, caso haja uma WorkHour correspondente!
        public short ContractType { get; set; } // Enum
        public short ProbationPeriodInMonths { get; set; }
        public short EmploymentStatus { get; set; } // Enum
        public DateOnly EndDay { get; set; }
    }
}

namespace HMS.ContractsMicroService.Application.DTOs.Input
{
    public class EmployeeContractInput
    {
        public EmployeeContractInput()
        {
            JobTitle = -1;
            ExperienceLevel = -1;
            Department = -1;
            HourlySalaryInDollar = 0;
            ContractType = -1;
            ProbationPeriodInMonths = -1;
            EmploymentStatus = -1;
        }
        public string EmplyoersName { get; set; } // Contratante
        public string EmploeeName { get; set; }
        public long PIS { get; set; }
        public DateTime StartDate { get; set; }
        public short JobTitle { get; set; } // Simucação de um banco de dados dedicado
        public sbyte ExperienceLevel { get; set; }
        public List<short> Benefits { get; set; } = new(); // Simulacão de beneficios que podem estar armazenados em um banco de dados
        public short Department { get; set; } //Enum ou um banco de dados dedicado
        public short HourlySalaryInDollar { get; set; } // Praticamente ninguem recebe +32k/hora
        public string WorkHoursID { get; set; } = string.Empty;
        public short ContractType { get; set; }// Enum
        public sbyte ProbationPeriodInMonths { get; set; }
        public short EmploymentStatus { get; set; } // Enum
        public DateOnly EndDay { get; set; }
    }
}

using Nuget.Employee.Inputs.Base;

namespace Nuget.Employee.Inputs
{
    public class ProfessionalHistoryInput : BaseWithEmployeeId
    {
        public string PreviousExperienceSummarized { get; set; }
        public string EducationLevel { get; set; } // Enum
        public string TrainingsAndCertifications { get; set; }
        public string PromotionHistory { get; set; } // Pensar
        public short PerformanceInPercentage { get; set; }
    }
}

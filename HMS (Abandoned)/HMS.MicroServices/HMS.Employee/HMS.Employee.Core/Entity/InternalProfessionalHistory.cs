using HMS.Employee.Core.Entity.Base;

namespace HMS.Employee.Core.Entity
{
    public sealed class InternalProfessionalHistory : BaseEntityWithEmployee
    {
        public string PreviousExperienceSummarized { get; set; }
        public string EducationLevel { get; set; } // Enum
        public string TrainingsAndCertifications { get; set; }
        public string PromotionHistory { get; set; } // Pensar
        public short PerformanceInPercentage { get; set; }
    }
}

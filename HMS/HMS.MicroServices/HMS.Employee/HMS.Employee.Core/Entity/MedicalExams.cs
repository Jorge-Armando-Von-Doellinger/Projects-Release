using HMS.Employee.Core.Entity.Base;

namespace HMS.Employee.Core.Entity
{
    public sealed class MedicalExams : BaseEntityWithEmployeeId
    {
        public string? ConditionSummary { get; set; } // Resumo da condição medica do funcionario
        public DateTime? LastExam { get; set; }
        public DateTime NextExam { get; set; }
    }
}

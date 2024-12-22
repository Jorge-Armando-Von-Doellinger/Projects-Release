using Nuget.Employee.Inputs.Base;

namespace Nuget.Employee.Inputs
{
    public class DependentDeclarationInput : BaseWithEmployeeId
    {
        public string Name { get; set; }
        public DateOnly BirthDate { get; set; }
        public long CPF { get; set; }
    }
}

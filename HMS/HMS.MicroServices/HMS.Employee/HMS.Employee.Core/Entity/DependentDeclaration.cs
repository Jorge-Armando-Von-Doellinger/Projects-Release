using HMS.Employee.Core.Entity.Base;
using Microsoft.VisualBasic;
using System.Runtime.CompilerServices;

namespace HMS.Employee.Core.Entity
{
    public sealed class DependentDeclaration : BaseEntityWithEmployeeId
    {
        public string Name { get; set; }
        public DateOnly BirthDate { get; set; }
        public long CPF { get; set; }
    }
}

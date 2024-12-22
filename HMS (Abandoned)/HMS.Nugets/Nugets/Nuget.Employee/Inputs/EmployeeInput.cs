using HMS.Employee.Application.JsonConverter;
using System.Text.Json.Serialization;

namespace Nuget.Employee.Inputs
{
    public class EmployeeInput
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public long CPF { get; set; }
        public long PIS { get; set; }
        public Int16 Age { get; set; }
        public DateOnly BirthDate { get; set; }
        public string MaritalStatus { get; set; }

        public Guid ContractId { get; set; }
    }
}

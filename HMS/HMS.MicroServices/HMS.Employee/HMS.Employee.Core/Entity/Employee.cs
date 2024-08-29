using HMS.Employee.Core.Entity.Base;

namespace HMS.Employee.Core.Entity
{
    public sealed class Employee : BaseEntity
    {
        // OBS: Optei por deixar cada micro servico isolado de outros banco de dados :)
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public long CPF { get; set; }
        public long PIS { get; set; }
        public Int16 Age { get; set; }
        public DateOnly BirthDate { get; set; }
        public string MaritalStatus { get; set; }

    }
}

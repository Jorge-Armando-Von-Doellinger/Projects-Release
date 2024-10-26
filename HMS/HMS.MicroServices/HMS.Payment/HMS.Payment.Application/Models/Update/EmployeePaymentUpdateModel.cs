using HMS.Payments.Application.Models.Input;

namespace HMS.Payments.Application.Models.Update
{
    //Herdam os valores de input padrões!
    public class EmployeePaymentUpdateModel : EmployeePaymentModel
    {
        public string ID { get; set; }
    }
}

using HMS.Payments.Application.Models.Input;

namespace HMS.Payments.Application.Models.Update;

// Herda o seu input
public sealed class PaymentUpdateModel : PaymentModel
{
    public string ID { get; set; }
}
namespace HMS.Payments.Application.Models.Input;

public class RefundModel
{
    public double Amount { get; set; }
    public string? Reason { get; set; }
    public string PaymentId { get; set; }
}
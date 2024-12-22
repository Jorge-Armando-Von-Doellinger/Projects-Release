namespace OmniSphere.Payments.Application.DTOs;

public class PaymentDTO
{
    public required string AccountId { get; set; }
    public double Amount { get; set; }
    public string? Message { get; set; }
}
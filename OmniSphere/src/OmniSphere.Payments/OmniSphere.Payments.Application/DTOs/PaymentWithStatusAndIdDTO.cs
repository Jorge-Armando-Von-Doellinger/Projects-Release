namespace OmniSphere.Payments.Application.DTOs;

public class PaymentWithStatusAndIdDTO : PaymentDTO
{
    public string PaymentId { get; set; }
    public int PaymentStatus {get; set;}
}
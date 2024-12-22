using System.ComponentModel.DataAnnotations;
using HMS.Payments.Core.Entity.Base;
using HMS.Payments.Core.Enums;

namespace HMS.Payments.Core.Entity;

public class Payment : EntityBase
{
    [StringLength(255, MinimumLength = 3,
        ErrorMessage = "Nome do beneficiario está inválido!")] // Limita o CPF em 14 caracteres
    public string Beneficiary { get; set; }

    [StringLength(255, MinimumLength = 3,
        ErrorMessage = "Nome do pagador está inválido!")] // Limita o CPF em 14 caracteres

    public string Payer { get; set; }

    public double Amount { get; set; }
    public PaymentMethodEnum PaymentMethod { get; set; }

    public string? Description { get; set; }

    [StringLength(18, MinimumLength = 14,
        ErrorMessage = "CNPJ do pagador está inválido!")] // Limita o CPF em 14 caracteres
    public string? PayerCPF { get; set; }

    [StringLength(18, MinimumLength = 18, ErrorMessage = "CNPJ do pagador está inválido!")]
    public string? PayerCNPJ { get; set; }

    [StringLength(14, MinimumLength = 14, ErrorMessage = "CPF do beneficiario está inválido!")]
    public string? BeneficiaryCPF { get; set; }

    [StringLength(18, MinimumLength = 18, ErrorMessage = "CNPJ do beneficiario está inválido!")]
    public string? BeneficiaryCNPJ { get; set; }

    public PaymentStatus Status { get; set; } = PaymentStatus.Pending;

    [StringLength(500, MinimumLength = 0,
        ErrorMessage = "Observações muito curtas! Por favor, nos dê mais informações!")]
    public string? Observations { get; set; }

    public void ValidateEntity()
    {
        base.ValidateEntity();
        ValidateData();
        ValidatePayment();
    }

    private void ValidateData()
    {
        // if (string.IsNullOrWhiteSpace(BeneficiaryCPF) && string.IsNullOrWhiteSpace(BeneficiaryCNPJ) ||
        //     (!string.IsNullOrWhiteSpace(BeneficiaryCPF) && !string.IsNullOrWhiteSpace(BeneficiaryCNPJ)))
        //      throw new InvalidDataException("Dados do beneficiario não são validos!");
        //
        // if (string.IsNullOrWhiteSpace(PayerCPF) && string.IsNullOrWhiteSpace(PayerCNPJ) ||
        //     (!string.IsNullOrWhiteSpace(PayerCPF) && !string.IsNullOrWhiteSpace(PayerCNPJ)))
        //     throw new InvalidDataException("Dados do pagador não são validos!");
    }

    private void ValidatePayment()
    {
        if (!Enum.IsDefined(typeof(PaymentMethodEnum), PaymentMethod))
            throw new ArgumentOutOfRangeException(nameof(PaymentMethod), "Metodo de pagamento inválido!");
        if (Amount <= 0) throw new InvalidDataException("O valor do pagamento não pode ser menor ou igual a zero!");
    }
}
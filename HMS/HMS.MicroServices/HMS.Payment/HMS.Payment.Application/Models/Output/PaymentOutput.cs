using HMS.Payments.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace HMS.Payments.Application.Models.Output
{
    // Saida padrão de pagamentos
    // É herdada pelas demais
    public class PaymentOutput
    {
        public string ID { get; set; }
        public string Beneficiary { get; set; }
        public string Payer { get; set; }
        public long Amount { get; set; }
        public string PaymentMethod { get; set; }
        public string? Description { get; set; }


        [StringLength(18, MinimumLength = 14, ErrorMessage = "CNPJ do pagador está inválido!")]// Limita o CPF em 14 caracteres
        public string? PayerCPF { get; set; }
        [StringLength(18, MinimumLength = 18, ErrorMessage = "CNPJ do pagador está inválido!")]
        public string? PayerCNPJ { get; set; }

        [StringLength(14, MinimumLength = 14, ErrorMessage = "CPF do beneficiario está inválido!")]
        public string? BeneficiaryCPF { get; set; }
        [StringLength(18, MinimumLength = 18, ErrorMessage = "CNPJ do beneficiario está inválido!")]
        public string? BeneficiaryCNPJ { get; set; }

        [StringLength(500, MinimumLength = 10, ErrorMessage = "Observações muito curtas! Por favor, nos dê mais informações!")]
        public string? Observations { get; set; }

        public void SetPaymentMethod(PaymentMethodEnum value)
        {
            PaymentMethod = value.ToString();
        }
    }
}

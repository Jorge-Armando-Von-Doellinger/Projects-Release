using System.ComponentModel.DataAnnotations;

namespace HMS.Payments.Application.Models.Input
{

    // Pagamento padrão, onde os outros irão herda-lo para se complementar!
    // É herdado pelo model de update
    public class PaymentModel
    {
        public string Beneficiary { get; set; }
        public string Payer { get; set; }
        public decimal Amount { get; set; }
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
    }
}

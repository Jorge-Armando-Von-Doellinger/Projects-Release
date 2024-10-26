using HMS.Payments.Core.Entity.Base;
using HMS.Payments.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace HMS.Payments.Core.Entity
{
    public class Payment : EntityBase
    {
        public string Beneficiary { get; set; }
        public string Payer { get; set; }
        public long Amount { get; set; }
        public PaymentMethodEnum PaymentMethod { get; set; }
        public string Description { get; set; }


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

        public bool ValidateEntity()
        {
            var payerInvalid = (PayerCNPJ == null || PayerCPF == null);
            var beneficiaryInvalid = (BeneficiaryCNPJ == null || BeneficiaryCPF == null);
            if (payerInvalid || beneficiaryInvalid || Amount < 1) return false;
            return true;
        }
    }
}

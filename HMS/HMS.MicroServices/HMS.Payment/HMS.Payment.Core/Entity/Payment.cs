using HMS.Payments.Core.Entity.Base;
using HMS.Payments.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace HMS.Payments.Core.Entity
{
    public class Payment : EntityBase
    {
        [StringLength(255, MinimumLength = 3, ErrorMessage = "Nome do beneficiario está inválido!")]// Limita o CPF em 14 caracteres
        public string Beneficiary { get; set; }
        [StringLength(255, MinimumLength = 3, ErrorMessage = "Nome do pagador está inválido!")]// Limita o CPF em 14 caracteres

        public string Payer { get; set; }
        public decimal Amount { get; set; }
        public string PaymentMethod { get; set; }

        public string Description { get; set; }


        [StringLength(18, MinimumLength = 14, ErrorMessage = "CNPJ do pagador está inválido!")]// Limita o CPF em 14 caracteres
        public string? PayerCPF { get; set; }
        [StringLength(18, MinimumLength = 18, ErrorMessage = "CNPJ do pagador está inválido!")]
        public string? PayerCNPJ { get; set; }

        [StringLength(14, MinimumLength = 14, ErrorMessage = "CPF do beneficiario está inválido!")]
        public string? BeneficiaryCPF { get; set; }
        [StringLength(18, MinimumLength = 18, ErrorMessage = "CNPJ do beneficiario está inválido!")]
        public string? BeneficiaryCNPJ { get; set; }
        public PaymentStatus Status { get; set; }

        [StringLength(500, MinimumLength = 10, ErrorMessage = "Observações muito curtas! Por favor, nos dê mais informações!")]
        public string? Observations { get; set; }

        public void ValidateEntity()
        {
            base.ValidateEntity();
            ValidateData();
            ValidatePayment();
        }

        private void ValidateData()
        {
            var payerDataIsValid = !(PayerCPF != null && PayerCNPJ != null) && (PayerCPF != null || PayerCNPJ != null); // Se ambos não sao NULL, mas se somente um for diferente de null, ta certo
            var beneficaryDataIsValid = !(BeneficiaryCPF != null && BeneficiaryCNPJ != null) && (BeneficiaryCPF != null || BeneficiaryCNPJ != null);
            
            if (payerDataIsValid == false) throw new InvalidDataException("Dados do pagador não são validos!");
            if (beneficaryDataIsValid == false) throw new InvalidDataException("Dados do beneficiario não são validos!");
        }

        private void ValidatePayment()
        {
            ArgumentNullException.ThrowIfNull(PaymentMethod);
            if (!Enum.IsDefined(typeof(PaymentMethodEnum), PaymentMethod)) throw new ArgumentOutOfRangeException(nameof(PaymentMethod), "Metodo de pagamento inválido!");
            if(Amount <= 0) throw new InvalidDataException("O valor do pagamento não pode ser menor ou igual a zero!");
        }
    }
}

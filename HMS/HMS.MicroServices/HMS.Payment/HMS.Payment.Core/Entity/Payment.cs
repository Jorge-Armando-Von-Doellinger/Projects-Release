using HMS.Payments.Core.Entity.Base;
using HMS.Payments.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace HMS.Payments.Core.Entity
{
    public class Payment : EntityBase
    {
        public string Beneficiary { get; set; }
        public string Payer { get; set; }
        public decimal Amount { get; set; }
        private string _paymentMethod;
        public string PaymentMethod
        {
            get => _paymentMethod;
            set
            {
                ArgumentNullException.ThrowIfNull(value);
                if (Enum.IsDefined(typeof(PaymentMethodEnum), value))
                    _paymentMethod = value;
                else throw new ArgumentOutOfRangeException(nameof(value), "Metodo de pagamento inválido!");
            }
        }

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

        public override void ValidateEntity()
        {
            ValidateData();
        }

        private void ValidateData()
        {
            var payerDataIsValid = PayerCPF != null || PayerCNPJ != null;
            var beneficaryDataIsValid = BeneficiaryCPF != null || BeneficiaryCNPJ != null;
            if (payerDataIsValid == false) throw new InvalidDataException("Dados do pagador não são validos!");
            if (beneficaryDataIsValid == false) throw new InvalidDataException("Dados do beneficiario não são validos!");
        }
    }
}

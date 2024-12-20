﻿using HMS.ContractsMicroService.Core.Entity.Base;
using HMS.ContractsMicroService.Core.Extensions;

namespace HMS.ContractsMicroService.Core.Entity
{
    public sealed class Contract : EntityBaseWithId
    {
        // Contratador
        public string ContractorName { get; set; }
        public string ContractorCPF { get; set; }
        public string ContractorAddress { get; set; }

        //Contratado
        public string HiredName { get; set; }
        public string HiredCPF { get; set; }
        public string HiredAddress { get; set; }
        //Assunto
        public string Subject { get; set; }
        private decimal _amount;
        public decimal Amount
        {
            get => _amount;
            set => _amount = Math.Round(value, 2);
        }
        public string PaymentMethod { get; set; } // As opções vão vir do micro servico de pagamento
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public string AdditionalClauses { get; set; }
        public string Jurisdiction { get; set; }

        public void Update(Contract valuesToReplace)
        {
            base.Update();
            if (valuesToReplace == null) Console.WriteLine("awdwasdd");
            this.Replacer(valuesToReplace, true);
        }
    }
}

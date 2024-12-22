namespace HMS.ContractsMicroService.Application.DTOs.Input
{
    public class ContractInput
    {
        public string ContractorName { get; set; }
        public string ContractorCPF { get; set; }
        public string ContractorAddress { get; set; }
        public string HiredName { get; set; }
        public string HiredCPF { get; set; }
        public string HiredAddress { get; set; }

        public string Subject { get; set; }
        public decimal Amount { get; set; }
        public string PaymentMethod { get; set; } // As opções vão vir do micro servico de pagamento
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public string AdditionalClauses { get; set; }
        public string Jurisdiction { get; set; }
    }
}

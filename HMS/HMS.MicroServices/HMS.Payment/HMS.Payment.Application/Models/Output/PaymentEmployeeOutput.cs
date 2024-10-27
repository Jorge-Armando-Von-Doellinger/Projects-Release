namespace HMS.Payments.Application.Models.Output
{
    // Model de saída dos pagamentos de funcionarios
    public sealed class PaymentEmployeeOutput : PaymentOutput
    {
        public string EmployeeId { get; set; } // Quando bater dia 5 ou dia 20, será buscado pelos empregados, onde serão capturados os IDs e realizará o pagamento com base nos dados apresentados!
        public int HourlySalary { get; set; } // Virá do contrato
        public short HoursWorkedInMonth { get; set; } // Virá do contrato
        public List<string> Benefits { get; set; } = new(); // Virá do contrato
        public int TotalAmountOfBenefits { get; set; } // Virá do contrato
    }
}

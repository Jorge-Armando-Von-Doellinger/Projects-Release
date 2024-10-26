namespace HMS.Payments.Core.Enums
{
    //[JsonConverter(typeof(JsonStringEnumConverter))]
    public enum BenefitsEnum
    {
        // Benefícios Obrigatórios
        PaidTimeOff, // Férias Pagas - Obrigatório pela CLT
        SickLeave, // Licença Médica - Obrigatório pela CLT
        PaidHolidays, // Feriados Pagos - Obrigatório pela CLT
        ParentalLeave, // Licença Parental - Obrigatório pela CLT
        CommuterBenefits, // Vale-Transporte - Obrigatório pela CLT
        ProfitSharing, // Participação nos Lucros - Obrigatório pela Lei 10.101/2000
        FGTS, // Fundo de Garantia por Tempo de Serviço - Obrigatório pela CLT
        INSS, // Contribuição para a Previdência Social - Obrigatório pela CLT
        OvertimePay, // Pagamento de Horas Extras - Obrigatório pela CLT
        ThirteenthSalary, // Décimo Terceiro Salário - Obrigatório pela CLT

        // Benefícios Não Obrigatórios (Opcional)
        HealthInsurance, // Seguro de Saúde
        DentalInsurance, // Seguro Odontológico
        VisionInsurance, // Seguro de Visão
        HealthSavingsAccount, // Conta de Saúde (HSA)
        WellnessProgram, // Programa de Bem-Estar
        RetirementPlan, // Plano de Aposentadoria (além do INSS)
        StockOptions, // Opções de Ações
        PerformanceBonus, // Bônus de Desempenho
        VolunteerTimeOff, // Tempo Livre para Voluntariado
        ProfessionalDevelopment, // Desenvolvimento Profissional
        TuitionReimbursement, // Reembolso de Matrícula
        CertificationReimbursement, // Reembolso de Certificação
        TrainingPrograms, // Programas de Treinamento
        EmployeeDiscounts, // Descontos para Funcionários
        GymMembership, // Associação de Academia
        CompanyCar, // Carro da Empresa
        ChildcareAssistance, // Assistência para Cuidados com Crianças
        MealVouchers, // Vale-Refeição
        RemoteWork, // Trabalho Remoto
        FlexibleSchedule // Horário Flexível
    }

}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HMS.ContractsMicroService.Core.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum BenefitsEnum
    {
        // Benefícios de Saúde
        HealthInsurance, // Seguro de Saúde
        DentalInsurance, // Seguro Odontológico
        VisionInsurance, // Seguro de Visão
        HealthSavingsAccount, // Conta de Saúde (HSA)
        WellnessProgram, // Programa de Bem-Estar

        // Benefícios Financeiros
        RetirementPlan, // Plano de Aposentadoria
        StockOptions, // Opções de Ações
        PerformanceBonus, // Bônus de Desempenho
        ProfitSharing, // Participação nos Lucros

        // Benefícios de Tempo Livre
        PaidTimeOff, // Tempo Livre Pago (Férias, Licença)
        SickLeave, // Licença Médica
        PaidHolidays, // Feriados Pagos
        ParentalLeave, // Licença Parental
        VolunteerTimeOff, // Tempo Livre para Voluntariado

        // Benefícios de Desenvolvimento e Treinamento
        ProfessionalDevelopment, // Desenvolvimento Profissional
        TuitionReimbursement, // Reembolso de Matrícula
        CertificationReimbursement, // Reembolso de Certificação
        TrainingPrograms, // Programas de Treinamento

        // Benefícios Adicionais
        CommuterBenefits, // Benefícios de Transporte
        EmployeeDiscounts, // Descontos para Funcionários
        GymMembership, // Associação de Academia
        CompanyCar, // Carro da Empresa
        ChildcareAssistance, // Assistência para Cuidados com Crianças
        MealVouchers, // Vale-Refeição
        RemoteWork, // Trabalho Remoto
        FlexibleSchedule // Horário Flexível
    }
}

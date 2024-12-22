namespace HMS.Payments.Infrastructure.Messages.Errors;

internal record DefaultErrorMessages
{
    internal static string PayrollNotFounded = "Nenhuma folha de pagamento encontrada!";
    internal static string ErrorInTransaction = "Houve um erro ao salvar seus dados";

    internal static string TimeOutInTransaction =
        "Há muitas transações no momento e ocorreu um timeout. Por favor, tente novamente!";
}
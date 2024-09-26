namespace HMS.ContractsMicroService.Infrastructure.Messages
{
    public record MessageRecords
    {
        public static string KeyNotFounded = "Identificador não encontrado!";
        internal static string Timeout = "Não foi possivel abrir uma transação no tempo esperado!";
    }
}

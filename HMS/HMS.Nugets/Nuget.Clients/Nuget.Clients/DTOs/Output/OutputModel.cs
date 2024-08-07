namespace Nuget.Clients.DTOs.Output
{
    public class OutputModel
    {
        // Pode ser extendido, mas, para um micro servico pequeno e, relativamente sem muitas necessidades,
        // esse model irá servir muito bem :)
        public OutputModel()
        {

        }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAdress { get; set; }
        public long CPF { get; set; }
        public long? RG { get; set; }
        public DateTime DateBirth { get; set; }
        public short YearsOld { get; set; }


    }
}

using HMS.Client.Models.Input;

namespace HMS.Client.Models.Output
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

    public class OutputModel<T>
    {
        // Para ele receber algum valor especifico, como o do Input
        // Pode ser usado para casos onde o Input e o Output serão iguais ou semelhantes!
        public T Value { get; set; }
        public OutputModel(T value)
        {
            Value = value;
        }
    }
}

using System.Runtime.CompilerServices;

namespace HMS.Core.Entity
{
    public class ClientEntity : BaseEntity
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAdress { get; set; }
        public long CPF { get; set; }
        public long? RG { get; set; }
        public DateTime DateBirth { get; set; }
        public short YearsOld { get; set; }


        public ClientEntity UpdateClient(ClientEntity existingClient)
        {
            if(Name == null)
                Name = existingClient.Name;
            if(PhoneNumber == null)
                PhoneNumber = existingClient.PhoneNumber;
            if(EmailAdress == null)
                EmailAdress = existingClient.EmailAdress;
            if(CPF == 0)
                CPF = existingClient.CPF;
            if(RG == null)
                RG = existingClient.RG;
            if(DateBirth == null)
                DateBirth = existingClient.DateBirth;
            CreatedAt = existingClient.CreatedAt;
            UpdatedAt = DateTime.Now.ToLocalTime();
            return this;
        }
    }
}

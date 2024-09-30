namespace HMS.ContractsMicroService.Core.Entity.Base
{
    public class EntityBaseWithId : EntityBase
    {
        public string ID { get; set; } = "Guid.NewGuid().ToString()";

        public void RecreateID()
            => ID = Guid.NewGuid().ToString();
    }
}

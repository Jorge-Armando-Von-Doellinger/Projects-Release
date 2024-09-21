namespace HMS.ContractsMicroService.Core.Entity.Base
{
    public class EntityBaseWithId : EntityBase
    {
        public Guid ID { get; set; } = Guid.NewGuid();
    }
}

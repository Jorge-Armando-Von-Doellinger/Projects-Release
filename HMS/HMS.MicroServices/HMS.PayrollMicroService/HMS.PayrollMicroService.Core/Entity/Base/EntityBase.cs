namespace HMS.PayrollMicroService.Core.Entity.Base
{
    public class EntityBase
    {
        public Guid ID { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        protected void Update() => UpdatedAt = DateTime.Now;
    }
}

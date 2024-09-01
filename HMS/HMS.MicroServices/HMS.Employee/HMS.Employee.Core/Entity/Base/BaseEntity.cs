namespace HMS.Employee.Core.Entity.Base
{
    public class BaseEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        public virtual void SetId(Guid ID)
        {
            if(Id == Guid.Empty)
                Id = ID;
        }

        public virtual void Update()
        {
            UpdatedAt = DateTime.Now;
        }
    }
}

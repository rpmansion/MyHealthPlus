namespace MyHealthPlus.Data.Models
{
    public class Role : EntityBase
    {
        public virtual string Name { get; set; }

        public virtual string NormalizedName { get; set; }

        public virtual string Description { get; set; }

        public virtual string ConcurrencyStamp { get; set; }
    }
}
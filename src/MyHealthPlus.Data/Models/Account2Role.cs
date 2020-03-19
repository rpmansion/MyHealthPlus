namespace MyHealthPlus.Data.Models
{
    public class Account2Role : EntityBase
    {
        public virtual Account Account { get; set; }

        public virtual Role Role { get; set; }
    }
}
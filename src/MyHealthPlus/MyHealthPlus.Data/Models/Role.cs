namespace MyHealthPlus.Data.Models
{
    public class Role : EntityBase
    {
        public string Name { get; set; }

        public string NormalizedName { get; set; }

        public string Description { get; set; }
    }
}
using System.Security.Claims;

namespace MyHealthPlus.Data.Models
{
    public class AccountClaim : EntityBase
    {
        public virtual Account Account { get; set; }

        public virtual string Type { get; set; }

        public virtual string Value { get; set; }

        public virtual Claim ToClaim()
        {
            return new Claim(Type, Value);
        }
    }
}
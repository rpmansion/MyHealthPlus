using System.Collections.Generic;

namespace MyHealthPlus.Data.Models
{
    public class Account : EntityBase
    {
        public Account()
        {
            AccountRoles = new List<Account2Role>();
        }

        public virtual string UserName { get; set; }

        public virtual string NormalizedUserName { get; set; }

        public virtual string PasswordHash { get; set; }

        public virtual string SecurityStamp { get; set; }

        public virtual string ConcurrencyStamp { get; set; }

        public virtual ICollection<Account2Role> AccountRoles { get; set; }
    }
}
using System.Collections.Generic;

namespace MyHealthPlus.Data.Models
{
    public class Account : EntityBase
    {
        public Account()
        {
            AccountRoles = new List<AccountRole>();
        }

        public string UserName { get; set; }

        public string PasswordHash { get; set; }

        public string SecurityStamp { get; set; }

        public ICollection<AccountRole> AccountRoles { get; set; }
    }
}
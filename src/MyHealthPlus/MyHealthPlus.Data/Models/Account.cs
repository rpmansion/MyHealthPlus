using System.Collections.Generic;

namespace MyHealthPlus.Data.Models
{
    public class Account : EntityBase
    {
        public Account()
        {
            AccountRoles = new List<Account2Role>();
        }

        public string UserName { get; set; }

        public string NormalizedUserName { get; set; }

        public string PasswordHash { get; set; }

        public string SecurityStamp { get; set; }

        public ICollection<Account2Role> AccountRoles { get; set; }
    }
}
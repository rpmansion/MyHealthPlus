using System;
using MyHealthPlus.Data.Enums;

namespace MyHealthPlus.Data.Models
{
    public class AccountProfile : EntityBase
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string MiddleName { get; set; }

        public DateTime BirthDate { get; set; }

        public string Contact { get; set; }

        public SexType SexType  { get; set; }

        public Account Account { get; set; }
    }
}
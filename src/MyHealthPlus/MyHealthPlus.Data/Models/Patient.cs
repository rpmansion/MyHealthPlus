using System;

namespace MyHealthPlus.Data.Models
{
    public class Patient : EntityBase
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string MiddleName { get; set; }

        public DateTime BirthDate { get; set; }

        public Appointment Appointment { get; set; }
    }
}
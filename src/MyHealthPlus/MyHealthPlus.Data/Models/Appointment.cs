using System;
using System.Collections.Generic;
using MyHealthPlus.Data.Enums;

namespace MyHealthPlus.Data.Models
{
    public class Appointment : EntityBase
    {
        public CheckupType CheckupType { get; set; }

        public DateTime Date { get; set; }

        public string Note { get; set; }

        // public ICollection<Patient> Patients { get; set; }
    }
}
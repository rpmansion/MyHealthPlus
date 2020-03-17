using MyHealthPlus.Data.Enums;
using System;

namespace MyHealthPlus.Data.Models
{
    public class Appointment : EntityBase
    {
        public virtual Account Account { get; set; }

        public CheckupType CheckupType { get; set; }

        public AppointmentStatus Status { get; set; }

        public DateTime Date { get; set; }

        public DateTime Time { get; set; }

        public string Note { get; set; }
    }
}
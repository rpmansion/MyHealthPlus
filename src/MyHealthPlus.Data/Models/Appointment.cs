using MyHealthPlus.Data.Enums;
using System;

namespace MyHealthPlus.Data.Models
{
    public class Appointment : EntityBase
    {
        public virtual Account Account { get; set; }

        public virtual CheckupType CheckupType { get; set; }

        public virtual AppointmentStatus Status { get; set; }

        public virtual DateTime Date { get; set; }

        public virtual DateTime Time { get; set; }

        public virtual string Note { get; set; }
    }
}
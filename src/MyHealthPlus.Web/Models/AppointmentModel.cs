using System;
using System.ComponentModel.DataAnnotations;
using MyHealthPlus.Data.Enums;

namespace MyHealthPlus.Web.Models
{
    public class AppointmentModel
    {
        [Required]
        public CheckupType CheckupType { get; set; }

        public string Note { get; set; }

        public DateTime AppointmentDate { get; set; }

        public DateTime AppoinmentTime { get; set; }
    }
}
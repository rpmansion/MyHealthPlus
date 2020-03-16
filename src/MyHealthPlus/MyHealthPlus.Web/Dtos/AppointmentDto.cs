using MyHealthPlus.Data.Enums;
using System;

namespace MyHealthPlus.Web.Dtos
{
    public class AppointmentDto
    {
        public CheckupType CeckupType { get; set; }

        public string Note { get; set; }

        public DateTime Date { get; set; }
    }
}
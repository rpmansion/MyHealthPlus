using System;

namespace MyHealthPlus.Core.Extensions.Dates
{
    public static class DateTimeExtensions
    {
        public static DateTime StartOfDay(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day, 0, 0, 0, 0, date.Kind);
        }

        public static DateTime EndOfDay(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day, 23, 59, 59, 999, date.Kind);
        }

        public static DateTime StartOfMonth(this DateTime d)
        {
            return d.SetDay(1);
        }

        public static DateTime EndOfMonth(this DateTime d)
        {
            return d.SetDay(DateTime.DaysInMonth(d.Year, d.Month));
        }

        public static DateTime SetDay(this DateTime d, int day)
        {
            return new DateTime(d.Year, d.Month, day, d.Hour, d.Minute, d.Second, d.Millisecond, d.Kind);
        }
    }
}
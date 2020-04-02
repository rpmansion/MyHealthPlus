using System;

namespace MyHealthPlus.Core.Extensions.Dates
{
    public interface IRange<T>
    {
        T Start { get; }

        T End { get; }

        bool Includes(T value);

        bool Includes(IRange<T> range);
    }

    public class DateTimeRange : IRange<DateTime>
    {
        public DateTimeRange(DateTime start, DateTime end)
        {
            if (start > end)
            {
                throw new ArgumentOutOfRangeException($"{nameof(start)} is greater than {nameof(end)}");
            }

            Start = start;
            End = end;
        }

        public DateTime Start { get; private set; }

        public DateTime End { get; private set; }

        public bool Includes(DateTime value)
        {
            return (value >= Start) && (value < End);
        }

        public bool Includes(IRange<DateTime> range)
        {
            return (range.Start >= Start) && (range.End < End);
        }
    }
}
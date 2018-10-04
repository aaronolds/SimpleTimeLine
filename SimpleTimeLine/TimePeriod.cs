using System;

namespace SimpleTimeLine
{
    public interface ITimePeriod
    {
        DateTime Start { get; }
        DateTime End { get; }
        bool HasStart { get; }
        bool HasEnd { get; }
        TimeSpan Duration { get; set; }

        void Setup(DateTime start, DateTime dateTime);
        bool IsSamePeriod(ITimePeriod test);
    }

    public class TimePeriod : ITimePeriod
    {
        private DateTime _end;
        private DateTime _start;

        public TimePeriod(DateTime start, DateTime end)
        {
            if (start > end) throw new ArgumentException($"Start Date {start} can not be greater than End Date {end}");

            _start = start;
            _end = end;
        }

        public DateTime Start => _start;

        public DateTime End => _end;

        public bool HasStart => _start != DateTime.MinValue;
        public bool HasEnd => _end != DateTime.MinValue;

        public TimeSpan Duration
        {
            get { return _end.Subtract(_start); }
            set
            {
                _end = _start.Add(value);
            }
        }

        public bool IsSamePeriod(ITimePeriod test)
        {
            if (test == null)
            {
                throw new ArgumentNullException("test");
            }
            return _start == test.Start && _end == test.End;
        }

        public void Setup(DateTime start, DateTime end)
        {
            _start = start;
            _end = end;
        }
    }
}

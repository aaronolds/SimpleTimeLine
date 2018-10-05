using System;

namespace SimpleTimeLine
{
    public interface ITimePeriod : ITimePeriodBase
    {
        // new DateTime Start { get; }
        // DateTime End { get; }
        // bool HasStart { get; }
        // bool HasEnd { get; }
        // TimeSpan Duration { get; set; }
        // string Description { get; set; }
        void Setup(DateTime start, DateTime dateTime);
        bool IsSamePeriod(ITimePeriod test);
        bool OverlapsWith(ITimePeriod test);
        bool HasInside(ITimePeriod test);
        bool HasInside(DateTime test);
        bool IntersectsWith(ITimePeriod test);
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

        public string Description { get; set; }

        public bool HasInside(ITimePeriod test)
        {
            if (test == null)
            {
                throw new ArgumentNullException("test");
            }
            return TimePeriodCalc.HasInside(this, test);
        }

        public bool HasInside(DateTime test)
        {
            if (test == null)
            {
                throw new ArgumentNullException("test");
            }
            return TimePeriodCalc.HasInside(this, test);
        }

        public bool IntersectsWith(ITimePeriod test)
        {
            if (test == null)
            {
                throw new ArgumentNullException("test");
            }
            return TimePeriodCalc.IntersectsWith(this, test);
        }

        public bool IsSamePeriod(ITimePeriod test)
        {
            if (test == null)
            {
                throw new ArgumentNullException("test");
            }
            return _start == test.Start && _end == test.End;
        }

        public bool OverlapsWith(ITimePeriod test)
        {
            if (test == null)
            {
                throw new ArgumentNullException("test");
            }
            return TimePeriodCalc.OverlapsWith(this, test);
        }

        public void Setup(DateTime start, DateTime end)
        {
            _start = start;
            _end = end;
        }
    }
}

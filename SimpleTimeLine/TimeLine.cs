using System;

namespace SimpleTimeLine
{
    public interface ITimeLine
    {
        bool IsOnTimeLine(DateTime test);
        bool IsOnTimeLine(ITimePeriod test);
        bool IntersectsWith(ITimePeriod test);
    }

    public class TimeLine : ITimeLine
    {
        private readonly ITimePeriodCollection _timePeriodCollection;

        public TimeLine(ITimePeriodCollection timePeriodCollection)
        {
            if (timePeriodCollection == null)
            {
                throw new ArgumentNullException("timePeriodCollection");
            }
            _timePeriodCollection = timePeriodCollection;
        }

        public bool IsOnTimeLine(DateTime test)
        {
            if (test == null)
            {
                throw new ArgumentNullException("test");
            }

            foreach (var period in _timePeriodCollection)
            {
                if (period.HasInside(test)) return true;
            }

            return false;
        }

        public bool IsOnTimeLine(ITimePeriod test)
        {
            if (test == null)
            {
                throw new ArgumentNullException("test");
            }

            foreach (var period in _timePeriodCollection)
            {
                if (period.HasInside(test)) return true;
            }

            return false;
        }

        public bool IntersectsWith(ITimePeriod test)
        {
            if (test == null)
            {
                throw new ArgumentNullException("test");
            }

            foreach (var period in _timePeriodCollection)
            {
                if (period.IntersectsWith(test)) return true;
            }

            return false;
        }
    }
}
using System;
using System.Collections;
using System.Collections.Generic;

namespace SimpleTimeLine
{
    public interface ITimePeriodCollection : IList<ITimePeriod>, ITimePeriodBase
    {
        bool ContainsPeriod(ITimePeriod test);
        void AddRange(IEnumerable<ITimePeriod> periods);
    }

    public class TimePeriodCollection : ITimePeriodCollection
    {
        private readonly List<ITimePeriod> periods = new List<ITimePeriod>();
        public TimePeriodCollection()
        {
        }

        public TimePeriodCollection(IEnumerable<ITimePeriod> periods) :
            this()
        {
            if (periods == null)
            {
                throw new ArgumentNullException("timePeriods");
            }
            AddRange(periods);
        }

        public ITimePeriod this[int index]
        {
            get { return periods[index]; }
            set { periods[index] = value; }
        }

        public int Count => periods.Count;

        public bool IsReadOnly { get; set; }

        public DateTime Start
        {
            get
            {
                DateTime? start = GetStart();
                return start.HasValue ? start.Value : DateTime.MinValue;
            }
            set
            {
                if (Count == 0)
                {
                    return;
                }
                Move(value - Start);
            }
        }

        public DateTime End
        {
            get
            {
                DateTime? end = GetEnd();
                return end.HasValue ? end.Value : DateTime.MaxValue;
            }
            set
            {
                if (Count == 0)
                {
                    return;
                }
                Move(value - End);
            }
        }

        public bool HasStart => Start != DateTime.MinValue;

        public bool HasEnd => End != DateTime.MaxValue;

        public TimeSpan Duration
        {
            get
            {
                TimeSpan? duration = GetDuration();
                return duration.HasValue ? duration.Value : DateTime.MaxValue - DateTime.MinValue;
            }
            set
            {
                throw new Exception("Duration cannot be set");
            }
        }

        public string Description { get; set; }

        public void Add(ITimePeriod item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }
            periods.Add(item);
        }

        public void AddRange(IEnumerable<ITimePeriod> periods)
        {
            if (periods == null)
            {
                throw new ArgumentNullException("timePeriods");
            }

            foreach (var period in periods)
            {
                Add(period);
            }
        }

        public void Clear()
        {
            periods.Clear();
        }

        public bool Contains(ITimePeriod item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }
            return periods.Contains(item);
        }

        public bool ContainsPeriod(ITimePeriod test)
        {
            if (test == null)
            {
                throw new ArgumentNullException("test");
            }

            foreach (ITimePeriod period in periods)
            {
                if (period.IsSamePeriod(test))
                {
                    return true;
                }
            }
            return false;
        }

        public void CopyTo(ITimePeriod[] array, int arrayIndex)
        {
            if (array == null)
            {
                throw new ArgumentNullException("array");
            }
            periods.CopyTo(array, arrayIndex);
        }

        public IEnumerator<ITimePeriod> GetEnumerator()
        {
            return periods.GetEnumerator();
        }

        public int IndexOf(ITimePeriod item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }
            return periods.IndexOf(item);
        }

        public void Insert(int index, ITimePeriod item)
        {
            if (index < 0 || index > Count)
            {
                throw new ArgumentOutOfRangeException("index");
            }
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }
            periods.Insert(index, item);
        }

        public bool Remove(ITimePeriod item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }
            return periods.Remove(item);
        }

        public void RemoveAt(int index)
        {
            periods.RemoveAt(index);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        protected virtual DateTime? GetStart()
        {
            if (Count == 0)
            {
                return null;
            }

            DateTime start = DateTime.MaxValue;

            foreach (ITimePeriod timePeriod in periods)
            {
                if (timePeriod.Start < start)
                {
                    start = timePeriod.Start;
                }
            }

            return start;
        }

        protected virtual DateTime? GetEnd()
        {
            if (Count == 0)
            {
                return null;
            }

            DateTime end = DateTime.MinValue;

            foreach (ITimePeriod timePeriod in periods)
            {
                if (timePeriod.End > end)
                {
                    end = timePeriod.End;
                }
            }

            return end;
        }

        public virtual void Move(TimeSpan delta)
        {
            if (delta == TimeSpan.Zero)
            {
                return;
            }

            foreach (ITimePeriod timePeriod in periods)
            {
                DateTime start = timePeriod.Start + delta;
                timePeriod.Setup(start, start.Add(timePeriod.Duration));
            }
        }

        protected virtual void GetStartEnd(out DateTime? start, out DateTime? end)
        {
            if (Count == 0)
            {
                start = null;
                end = null;
                return;
            }

            start = DateTime.MaxValue;
            end = DateTime.MinValue;

            foreach (ITimePeriod timePeriod in periods)
            {
                if (timePeriod.Start < start)
                {
                    start = timePeriod.Start;
                }
                if (timePeriod.End > end)
                {
                    end = timePeriod.End;
                }
            }
        }

        protected virtual TimeSpan? GetDuration()
        {
            DateTime? start;
            DateTime? end;

            GetStartEnd(out start, out end);

            if (start == null || end == null)
            {
                return null;
            }

            return end.Value - start.Value;
        }

        public void Setup(DateTime start, DateTime dateTime)
        {
            throw new NotImplementedException();
        }
    }
}
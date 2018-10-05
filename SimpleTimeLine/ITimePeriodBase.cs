using System;

namespace SimpleTimeLine
{
    public interface ITimePeriodBase
    {
        DateTime Start { get; }
        DateTime End { get; }
        bool HasStart { get; }
        bool HasEnd { get; }
        TimeSpan Duration { get; set; }
        string Description { get; set; }
    }
}
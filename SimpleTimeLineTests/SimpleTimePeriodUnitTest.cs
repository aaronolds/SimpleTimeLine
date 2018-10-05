using System;
using Xunit;

namespace SimpleTimeLine.Test
{
    public sealed class SimpleTimePeriodUnitTest
    {
        [Fact]
        public void TimePeriodObjectTest()
        {
            var timePeriod = new SimpleTimeLine.TimePeriod(DateTime.MinValue, DateTime.MinValue);
            Assert.IsType<SimpleTimeLine.TimePeriod>(timePeriod);
            Assert.False(timePeriod.HasStart);
            Assert.False(timePeriod.HasEnd);
        }

        [Fact]
        public void TimePeriodObjectWithEndDateLesThanStartDate()
        {
            var ex = Assert.Throws<ArgumentException>(() => new SimpleTimeLine.TimePeriod(DateTime.MaxValue, DateTime.MinValue));
            Assert.Equal($"Start Date {DateTime.MaxValue} can not be greater than End Date {DateTime.MinValue}", ex.Message);
        }

        [Fact]
        public void TimePeriodObjectWithStartDateLessThanEndDate()
        {
            var timePeriod = new SimpleTimeLine.TimePeriod(DateTime.MinValue, DateTime.MaxValue);
            Assert.Equal(DateTime.MinValue, timePeriod.Start);
            Assert.Equal(DateTime.MaxValue, timePeriod.End);
        }

        [Fact]
        public void TimePeriodObjectWithRealDates()
        {
            DateTime start = new DateTime(2018, 2, 3);
            DateTime end = new DateTime(2018, 8, 9);
            var timePeriod = new SimpleTimeLine.TimePeriod(start, end);
            Assert.Equal(start, timePeriod.Start);
            Assert.Equal(end, timePeriod.End);
            Assert.True(timePeriod.HasStart);
            Assert.True(timePeriod.HasEnd);
        }

        [Fact]
        public void TimePeriodDurationTest()
        {
            DateTime start = new DateTime(2018, 1, 1);
            DateTime end = new DateTime(2018, 12, 31);
            var timePeriod = new SimpleTimeLine.TimePeriod(start, end);

            var x = timePeriod.Duration;
            Assert.Equal(new TimeSpan(364, 0, 0, 0), x);
        }

        [Fact]
        public void TimePeriodIsSamePeriodThrowErrorWithNullTest()
        {
            DateTime start = new DateTime(2018, 1, 1);
            DateTime end = new DateTime(2018, 12, 31);
            var timePeriod = new SimpleTimeLine.TimePeriod(start, end);
            Assert.Throws<ArgumentNullException>(() => timePeriod.IsSamePeriod(null));
            Assert.Throws<ArgumentNullException>(() => timePeriod.OverlapsWith(null));
            Assert.Throws<ArgumentNullException>(() => timePeriod.IntersectsWith(null));
            Assert.Throws<ArgumentNullException>(() => timePeriod.HasInside(null));
        }

        [Fact]
        public void TimePeriodIsSamePeriodTest()
        {
            DateTime start = new DateTime(2018, 1, 1);
            DateTime end = new DateTime(2018, 12, 31);
            var timePeriod = new SimpleTimeLine.TimePeriod(start, end);

            var test = new SimpleTimeLine.TimePeriod(start, end);
            Assert.True(timePeriod.IsSamePeriod(test));
        }

        [Fact]
        public void TimePeriodIsNotSamePeriodTest()
        {
            DateTime start = new DateTime(2018, 1, 1);
            DateTime end = new DateTime(2018, 12, 31);
            var timePeriod = new SimpleTimeLine.TimePeriod(start, end);

            var test = new SimpleTimeLine.TimePeriod(start.AddDays(1), end.AddDays(1));
            Assert.False(timePeriod.IsSamePeriod(test));
        }

        [Fact]
        public void TimePeriodOverlapTimePeriod()
        {
            DateTime start = new DateTime(2018, 1, 1);
            DateTime end = new DateTime(2018, 12, 31);
            var timePeriod = new SimpleTimeLine.TimePeriod(start, end);

            DateTime start1 = new DateTime(2018, 6, 1);
            DateTime end1 = new DateTime(2019, 2, 20);
            var timePeriod1 = new SimpleTimeLine.TimePeriod(start1, end1);

            Assert.True(timePeriod.OverlapsWith(timePeriod1));
        }

        [Fact]
        public void TimePeriodRelationEndInside()
        {
            DateTime start = new DateTime(2018, 1, 1);
            DateTime end = new DateTime(2018, 12, 31);
            var timePeriod = new SimpleTimeLine.TimePeriod(start, end);

            DateTime start1 = new DateTime(2018, 6, 1);
            DateTime end1 = new DateTime(2019, 2, 20);
            var timePeriod1 = new SimpleTimeLine.TimePeriod(start1, end1);

            Assert.Equal(PeriodRelation.EndInside, TimePeriodCalc.GetRelation(timePeriod, timePeriod1));
        }

        [Fact]
        public void TimePeriodRelationAfter()
        {
            DateTime start = new DateTime(2018, 1, 1);
            DateTime end = new DateTime(2018, 12, 31);
            var timePeriod = new SimpleTimeLine.TimePeriod(start, end);

            DateTime start1 = new DateTime(2017, 2, 1);
            DateTime end1 = new DateTime(2017, 2, 20);
            var timePeriod1 = new SimpleTimeLine.TimePeriod(start1, end1);

            Assert.Equal(PeriodRelation.After, TimePeriodCalc.GetRelation(timePeriod, timePeriod1));
        }

        [Fact]
        public void TimePeriodRelationEnd()
        {
            DateTime start = new DateTime(2018, 1, 1);
            DateTime end = new DateTime(2018, 12, 31);
            var timePeriod = new SimpleTimeLine.TimePeriod(start, end);

            DateTime start1 = new DateTime(2019, 2, 1);
            DateTime end1 = new DateTime(2019, 2, 20);
            var test = new SimpleTimeLine.TimePeriod(start1, end1);

            Assert.Equal(PeriodRelation.Before, TimePeriodCalc.GetRelation(timePeriod, test));
        }

        [Fact]
        public void TimePeriodRelationExactMatch()
        {
            DateTime start = new DateTime(2018, 1, 1);
            DateTime end = new DateTime(2018, 12, 31);
            var timePeriod = new SimpleTimeLine.TimePeriod(start, end);

            DateTime start1 = new DateTime(2018, 1, 1);
            DateTime end1 = new DateTime(2018, 12, 31);
            var test = new SimpleTimeLine.TimePeriod(start1, end1);

            Assert.Equal(PeriodRelation.ExactMatch, TimePeriodCalc.GetRelation(timePeriod, test));
        }

        [Fact]
        public void TimePeriodRelationStartTouching()
        {
            DateTime start = new DateTime(2018, 1, 1);
            DateTime end = new DateTime(2018, 12, 31);
            var timePeriod = new SimpleTimeLine.TimePeriod(start, end);

            DateTime start1 = new DateTime(2017, 1, 1);
            DateTime end1 = new DateTime(2018, 1, 1);
            var test = new SimpleTimeLine.TimePeriod(start1, end1);

            Assert.Equal(PeriodRelation.StartTouching, TimePeriodCalc.GetRelation(timePeriod, test));
        }

        [Fact]
        public void TimePeriodRelationEndTouching()
        {
            DateTime start = new DateTime(2018, 1, 1);
            DateTime end = new DateTime(2018, 12, 31);
            var timePeriod = new SimpleTimeLine.TimePeriod(start, end);

            DateTime start1 = new DateTime(2018, 12, 31);
            DateTime end1 = new DateTime(2019, 2, 1);
            var test = new SimpleTimeLine.TimePeriod(start1, end1);

            Assert.Equal(PeriodRelation.EndTouching, TimePeriodCalc.GetRelation(timePeriod, test));
        }
    }
}

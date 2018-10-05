using System;
using System.Collections.Generic;
using Xunit;

namespace SimpleTimeLine.Test
{
    public class SimpleTimeLineUnitTest
    {
        private SimpleTimeLine.ITimePeriodCollection EmploymentTimeLine(int year)
        {
            var timePeriodList = new List<SimpleTimeLine.ITimePeriod>();
            timePeriodList.Add(new SimpleTimeLine.TimePeriod(new DateTime(1999, 5, 8), new DateTime(year, 4, 8)));
            timePeriodList.Add(new SimpleTimeLine.TimePeriod(new DateTime(year, 8, 15), DateTime.MaxValue));

            return new SimpleTimeLine.TimePeriodCollection(timePeriodList);
        }

        [Fact]
        public void TimeLineObject()
        {
            var timePeriodCollection = EmploymentTimeLine(2018);

            var timeLine = new SimpleTimeLine.TimeLine(timePeriodCollection); ;
            Assert.IsType<SimpleTimeLine.TimeLine>(timeLine);
        }

        [Fact]
        public void TimeLineObjectNull()
        {
            Assert.Throws<ArgumentNullException>(() => new SimpleTimeLine.TimeLine(null));
        }

        [Fact]
        public void TimeLineIsDateOnTimeLine()
        {
            var timePeriodCollection = EmploymentTimeLine(2018);

            var timeLine = new SimpleTimeLine.TimeLine(timePeriodCollection);
            Assert.True(timeLine.IsOnTimeLine(new DateTime(2018, 1, 1)));
        }

        [Fact]
        public void TimeLineIsDateOnTimeLineFalse()
        {
            var timePeriodCollection = EmploymentTimeLine(2018);

            var timeLine = new SimpleTimeLine.TimeLine(timePeriodCollection);
            Assert.False(timeLine.IsOnTimeLine(new DateTime(1998, 1, 1)));
        }

        [Fact]
        public void TimeLinePeriodIntersects()
        {
            var timePeriodCollection = EmploymentTimeLine(2018);

            var timeLine = new SimpleTimeLine.TimeLine(timePeriodCollection);
            var period = new TimePeriod(new DateTime(2018, 1, 1), new DateTime(2018, 1, 31));
            Assert.True(timeLine.IntersectsWith(period));
        }


        [Fact]
        public void TimeLinePeriodIntersectsFalse()
        {
            var timePeriodCollection = EmploymentTimeLine(2018);

            var timeLine = new SimpleTimeLine.TimeLine(timePeriodCollection);
            var period = new TimePeriod(new DateTime(2018, 6, 1), new DateTime(2018, 6, 30));
            Assert.False(timeLine.IntersectsWith(period));
        }

        [Fact]
        public void TimeLineIsTimePeriodOnTimeLine()
        {
            var timePeriodCollection = EmploymentTimeLine(2018);

            var timeLine = new SimpleTimeLine.TimeLine(timePeriodCollection);
            var period = new TimePeriod(new DateTime(2018, 1, 1), new DateTime(2018, 1, 31));
            Assert.True(timeLine.IsOnTimeLine(period));
        }
    }
}
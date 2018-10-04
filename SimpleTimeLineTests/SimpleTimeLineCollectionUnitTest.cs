using System;
using System.Collections.Generic;
using Xunit;

namespace SimpleTimeLine.Test
{
    public sealed class SimpleTimePeriodCollectionUnitTest
    {
        private SimpleTimeLine.ITimePeriodCollection GetTimePeriodCollection()
        {
            DateTime start = new DateTime(2018, 2, 3);
            DateTime end = new DateTime(2018, 8, 9);
            var timePeriodList = new List<ITimePeriod>();
            timePeriodList.Add(new SimpleTimeLine.TimePeriod(start, end));
            timePeriodList.Add(new SimpleTimeLine.TimePeriod(start.AddMonths(1), end.AddMonths(1)));
            timePeriodList.Add(new SimpleTimeLine.TimePeriod(start.AddMonths(2), end.AddMonths(2)));
            timePeriodList.Add(new SimpleTimeLine.TimePeriod(start.AddMonths(3), end.AddMonths(3)));

            return new SimpleTimeLine.TimePeriodCollection(timePeriodList);
        }

        [Fact]
        public void TimePeriodCollectionObjectTest()
        {
            var timePeriodCollection = new SimpleTimeLine.TimePeriodCollection();
            Assert.IsType<SimpleTimeLine.TimePeriodCollection>(timePeriodCollection);
            Assert.Equal(DateTime.MinValue, timePeriodCollection.Start);
            Assert.Equal(DateTime.MaxValue, timePeriodCollection.End);
            Assert.False(timePeriodCollection.HasStart);
            Assert.False(timePeriodCollection.HasEnd);
        }

        [Fact]
        public void TimePeriodCollectionObjectWithPeriodsTest()
        {
            var timePeriodCollection = GetTimePeriodCollection();
            Assert.IsType<SimpleTimeLine.TimePeriodCollection>(timePeriodCollection);
        }

        [Fact]
        public void TimePeriodCollectionObjectWithPeriodsAreNullTest()
        {
            Assert.Throws<ArgumentNullException>(() => new SimpleTimeLine.TimePeriodCollection(null));
        }

        [Fact]
        public void TimePeriodCollectionGetCountOfPeriods()
        {
            var timePeriodCollection = GetTimePeriodCollection();
            Assert.Equal(4, timePeriodCollection.Count);
        }

        [Fact]
        public void TimePeriodCollectionGetItemByIndexer()
        {
            var timePeriodCollection = GetTimePeriodCollection();
            var item = timePeriodCollection[2];
            Assert.IsType<SimpleTimeLine.TimePeriod>(item);
        }


        [Fact]
        public void TimePeriodCollectionSetItemByIndexer()
        {
            DateTime start = new DateTime(2020, 2, 3);
            DateTime end = new DateTime(2020, 8, 9);
            var newItem = new SimpleTimeLine.TimePeriod(start.AddMonths(3), end.AddMonths(3));

            var timePeriodCollection = GetTimePeriodCollection();
            timePeriodCollection[2] = newItem;
            var item = timePeriodCollection[2];
            Assert.Equal(newItem, item);
        }

        [Fact]
        public void TimePeriodCollectionGetStartAndGetEnd()
        {
            var timePeriodCollection = GetTimePeriodCollection();
            var start = timePeriodCollection.Start;
            var end = timePeriodCollection.End;
            Assert.Equal(new DateTime(2018, 2, 3), start);
            Assert.Equal(new DateTime(2018, 11, 9), end);
        }

        [Fact]
        public void TimePeriodCollectionGetDuration()
        {
            var timePeriodCollection = GetTimePeriodCollection();
            Assert.Equal(new TimeSpan(279, 00, 00, 00), timePeriodCollection.Duration);
        }
    }
}
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

        [Fact]
        public void TimePeriodCollectionCountAndClear()
        {
            var timePeriodCollection = GetTimePeriodCollection();
            Assert.True(timePeriodCollection.Count > 0);
            timePeriodCollection.Clear();
            Assert.True(timePeriodCollection.Count == 0);
        }

        [Fact]
        public void TimePeriodCollectionContains()
        {
            var timePeriodCollection = GetTimePeriodCollection();

            DateTime start = new DateTime(2018, 2, 3);
            DateTime end = new DateTime(2018, 8, 9);
            var timePeriod = new SimpleTimeLine.TimePeriod(start, end);
            Assert.True(timePeriodCollection.ContainsPeriod(timePeriod));
        }

        [Fact]
        public void TimePeriodCollectionContainsFalse()
        {
            var timePeriodCollection = GetTimePeriodCollection();

            DateTime start = new DateTime(2018, 1, 3);
            DateTime end = new DateTime(2018, 8, 9);
            var timePeriod = new SimpleTimeLine.TimePeriod(start, end);
            Assert.False(timePeriodCollection.ContainsPeriod(timePeriod));
        }

        [Fact]
        public void TimePeriodCollectionContainsNull()
        {
            var timePeriodCollection = GetTimePeriodCollection();
            Assert.Throws<ArgumentNullException>(() => timePeriodCollection.ContainsPeriod(null));
        }

        [Fact]
        public void TimePeriodCollectionInsert()
        {
            var timePeriodCollection = GetTimePeriodCollection();

            DateTime start = new DateTime(2018, 1, 3);
            DateTime end = new DateTime(2018, 8, 9);
            var timePeriod = new SimpleTimeLine.TimePeriod(start, end);
            timePeriodCollection.Insert(0, timePeriod);
            //Assert.False(timePeriodCollection.ContainsPeriod(timePeriod));
            Assert.Equal(timePeriod, timePeriodCollection[0]);
        }

        [Fact]
        public void TimePeriodCollectionInsertArguemntOutOfRange()
        {
            var timePeriodCollection = GetTimePeriodCollection();

            DateTime start = new DateTime(2018, 1, 3);
            DateTime end = new DateTime(2018, 8, 9);
            var timePeriod = new SimpleTimeLine.TimePeriod(start, end);
            Assert.Throws<ArgumentOutOfRangeException>(() => timePeriodCollection.Insert(-1, timePeriod));
        }

       [Fact]
        public void TimePeriodCollectionInsertArguemntNull()
        {
            var timePeriodCollection = GetTimePeriodCollection();

            Assert.Throws<System.ArgumentNullException>(() => timePeriodCollection.Insert(0, null));
        }
    }
}
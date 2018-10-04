namespace SimpleTimeLine
{
    public interface ITimeLine
    {
        void AddTimePeriod(ITimePeriod timePeriod);
    }

    public class TimeLine : ITimeLine
    {
        public void AddTimePeriod(ITimePeriod timePeriod)
        {
            throw new System.NotImplementedException();
        }
    }
}
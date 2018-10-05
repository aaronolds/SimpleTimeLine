using System;

namespace SimpleTimeLine
{
   internal static class TimePeriodCalc
	{

		public static bool HasInside( ITimePeriod period, DateTime test )
		{
			return test >= period.Start && test <= period.End;
		} 

		public static bool HasInside( ITimePeriod period, ITimePeriod test )
		{
			return HasInside( period, test.Start ) && HasInside( period, test.End );
		} 

		public static bool IntersectsWith( ITimePeriod period, ITimePeriod test )
		{
			return
				HasInside( period, test.Start ) ||
				HasInside( period, test.End ) ||
				( test.Start < period.Start && test.End > period.End );
		} 

		public static bool OverlapsWith( ITimePeriod period, ITimePeriod test )
		{
			PeriodRelation relation = GetRelation( period, test );
			return
				relation != PeriodRelation.After &&
				relation != PeriodRelation.StartTouching &&
				relation != PeriodRelation.EndTouching &&
				relation != PeriodRelation.Before;
		} 

		public static PeriodRelation GetRelation( ITimePeriod period, ITimePeriod test )
		{
			if ( test.End < period.Start )
			{
				return PeriodRelation.After;
			}
			if ( test.Start > period.End )
			{
				return PeriodRelation.Before;
			}
			if ( test.Start == period.Start && test.End == period.End )
			{
				return PeriodRelation.ExactMatch;
			}
			if ( test.End == period.Start )
			{
				return PeriodRelation.StartTouching;
			}
			if ( test.Start == period.End )
			{
				return PeriodRelation.EndTouching;
			}
			if ( HasInside( period, test ) )
			{
				if ( test.Start == period.Start )
				{
					return PeriodRelation.EnclosingStartTouching;
				}
				return test.End == period.End ? PeriodRelation.EnclosingEndTouching : PeriodRelation.Enclosing;
			}
			bool periodContainsMyStart = HasInside( test, period.Start );
			bool periodContainsMyEnd = HasInside( test, period.End );
			if ( periodContainsMyStart && periodContainsMyEnd )
			{
				if ( test.Start == period.Start )
				{
					return PeriodRelation.InsideStartTouching;
				}
				return test.End == period.End ? PeriodRelation.InsideEndTouching : PeriodRelation.Inside;
			}
			if ( periodContainsMyStart )
			{
				return PeriodRelation.StartInside;
			}
			if ( periodContainsMyEnd )
			{
				return PeriodRelation.EndInside;
			}
			throw new InvalidOperationException( "invalid period relation of '" + period + "' and '" + test + "'" );
		} 
	}
}
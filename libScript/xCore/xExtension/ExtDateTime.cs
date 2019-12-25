#if xLibv3
using System;

namespace xLib
{
	public static class ExtDateTime
	{
		public static double TotalSeconds(this DateTime dateTime)
		{
			return (dateTime.Ticks*0.0000001f);
		}
		
		public static DateTime StartOfDay(this DateTime dateTime)
		{
			return dateTime.Date;
		}
		
		public static DateTime StartOfWeek(this DateTime dateTime,DayOfWeek startDayOfWeek=DayOfWeek.Monday)
		{
			int deltaDay = 0;
			deltaDay = dateTime.DayOfWeek-startDayOfWeek;
			deltaDay += 7;
			deltaDay %= 7;
			return dateTime.AddDays(-deltaDay).Date;
		}
		
		public static DateTime StartOfMonth(this DateTime dateTime)
		{
			return dateTime.AddDays(-dateTime.Day).Date;
		}
	}
}
#endif


// public static DateTime Next(this DateTime from, DayOfWeek dayOfWeek)
// {
// 	int start = (int)from.DayOfWeek;
// 	int target = (int)dayOfWeek;
	
// 	if (target <= start) target += 7;
	
// 	return from.AddDays(target - start);
// }
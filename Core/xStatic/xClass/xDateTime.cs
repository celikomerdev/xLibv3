#if xLibv3
using System;
using UnityEngine;

namespace xLib
{
	[Serializable]
	public struct xDateTime
	{
		[SerializeField]private int year;
		[SerializeField]private int month;
		[SerializeField]private int day;
		[SerializeField]private int hour;
		[SerializeField]private int minute;
		[SerializeField]private int second;
		public DateTime dateTime;
		
		public xDateTime(DateTime dateTime)
		{
			this.year = dateTime.Year;
			this.month = dateTime.Month;
			this.day = dateTime.Day;
			this.hour = dateTime.Hour;
			this.minute = dateTime.Minute;
			this.second = dateTime.Second;
			this.dateTime = dateTime;
		}
		
		public void Init()
		{
			this.dateTime = new DateTime(year,month,day,hour,minute,second);
		}
		
		#region StartOfDateTime
		public static DateTime StartOfWeek(DateTime dateTime,DayOfWeek startDayOfWeek=DayOfWeek.Monday)
		{
			int deltaDay = 0;
			deltaDay = dateTime.DayOfWeek-startDayOfWeek;
			deltaDay += 7;
			deltaDay %= 7;
			return dateTime.AddDays(-deltaDay).Date;
		}
		#endregion
	}
}
#endif
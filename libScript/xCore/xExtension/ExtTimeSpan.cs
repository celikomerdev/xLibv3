#if xLibv3
using System;

namespace xLib
{
	public static class ExtTimeSpan
	{
		public static string ToMax(this TimeSpan timeSpan,string format="")
		{
			if(timeSpan.TotalDays>365)
			{
				return string.Format(MnLocalize.GetValue("{0} Years"),(timeSpan.TotalDays/365).ToString(format));
			}
			
			if(timeSpan.TotalDays>30)
			{
				return string.Format(MnLocalize.GetValue("{0} Months"),(timeSpan.TotalDays/30).ToString(format));
			}
			
			if(timeSpan.TotalDays>7)
			{
				return string.Format(MnLocalize.GetValue("{0} Weeks"),(timeSpan.TotalDays/7).ToString(format));
			}
			
			if(timeSpan.TotalDays>1)
			{
				return string.Format(MnLocalize.GetValue("{0} Days"),timeSpan.TotalDays.ToString(format));
			}
			
			if(timeSpan.TotalHours>1)
			{
				return string.Format(MnLocalize.GetValue("{0} Hours"),timeSpan.TotalHours.ToString(format));
			}
			
			if(timeSpan.TotalMinutes>1)
			{
				return string.Format(MnLocalize.GetValue("{0} Minutes"),timeSpan.TotalMinutes.ToString(format));
			}
			
			if(timeSpan.TotalSeconds>1)
			{
				return string.Format(MnLocalize.GetValue("{0} Seconds"),timeSpan.TotalSeconds.ToString(format));
			}
			
			return "-";
		}
	}
}
#endif

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
				return MnLocalize.GetValue("{Years} Years").Replace("{Years}",(timeSpan.TotalDays/365).ToString(format));
			}
			
			if(timeSpan.TotalDays>30)
			{
				return MnLocalize.GetValue("{Months} Months").Replace("{Months}",(timeSpan.TotalDays/30).ToString(format));
			}
			
			if(timeSpan.TotalDays>7)
			{
				return MnLocalize.GetValue("{Weeks} Weeks").Replace("{Weeks}",(timeSpan.TotalDays/7).ToString(format));
			}
			
			if(timeSpan.TotalDays>1)
			{
				return MnLocalize.GetValue("{Days} Days").Replace("{Days}",timeSpan.TotalDays.ToString(format));
			}
			
			if(timeSpan.TotalHours>1)
			{
				return MnLocalize.GetValue("{Hours} Hours").Replace("{Hours}",timeSpan.TotalHours.ToString(format));
			}
			
			if(timeSpan.TotalMinutes>1)
			{
				return MnLocalize.GetValue("{Minutes} Minutes").Replace("{Minutes}",timeSpan.TotalMinutes.ToString(format));
			}
			
			if(timeSpan.TotalSeconds>1)
			{
				return MnLocalize.GetValue("{Seconds} Seconds").Replace("{Seconds}",timeSpan.TotalSeconds.ToString(format));
			}
			
			return "-";
		}
	}
}
#endif

#if xLibv3
using System;

namespace xLib
{
	public static class ExtTimeSpan
	{
		// string format = @"dd\:hh\:mm\:ss";
		// string formatted = timeSpan.ToString(@format);
			
		public static string ToStringCustom(this TimeSpan timeSpan,string format)
		{
			string temp = format;
			temp = temp.Replace("DD",((int)timeSpan.TotalDays).ToString("00"));
			temp = temp.Replace("dd",timeSpan.Days.ToString("00"));
			
			temp = temp.Replace("HH",((int)timeSpan.TotalHours).ToString("00"));
			temp = temp.Replace("hh",timeSpan.Hours.ToString("00"));
			
			temp = temp.Replace("MM",((int)timeSpan.TotalMinutes).ToString("00"));
			temp = temp.Replace("mm",timeSpan.Minutes.ToString("00"));
			
			temp = temp.Replace("SS",((int)timeSpan.TotalSeconds).ToString("00"));
			temp = temp.Replace("ss",timeSpan.Seconds.ToString("00"));
			
			temp = temp.Replace("ff",timeSpan.Milliseconds.ToString("00"));
			return temp;
		}
		
		// public static string ToStringNew(this TimeSpan ts,string format)
		// {
		// 	System.Text.StringBuilder sb = new System.Text.StringBuilder();
		// 	sb.Append(format);
		// 	return sb.ToString();
		// }
	}
}
#endif

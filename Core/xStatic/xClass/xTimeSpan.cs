#if xLibv3
using System;

namespace xLib
{
	[Serializable]
	public class xTimeSpan
	{
		public static string ToStringCustom(TimeSpan timeSpan,string format)
		{
			string temp = format;
			temp = temp.Replace("dd",timeSpan.Days.ToString("00"));
			temp = temp.Replace("hh",timeSpan.Hours.ToString("00"));
			temp = temp.Replace("mm",timeSpan.Minutes.ToString("00"));
			temp = temp.Replace("ss",timeSpan.Seconds.ToString("00"));
			return temp;
		}
	}
}
#endif
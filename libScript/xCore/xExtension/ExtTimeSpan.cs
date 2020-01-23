#if xLibv3
using System;
using UnityEngine;

namespace xLib
{
	public static class ExtTimeSpan
	{
		public static string ToMax(this TimeSpan timeSpan,string format="")
		{
			if(timeSpan.TotalDays>365)
			{
				return string.Format(MnLocalize.GetValue("{0} Years"),Mathf.RoundToInt((float)timeSpan.TotalDays/365).ToString(format));
			}
			
			if(timeSpan.TotalDays>30)
			{
				return string.Format(MnLocalize.GetValue("{0} Months"),Mathf.RoundToInt((float)timeSpan.TotalDays/30).ToString(format));
			}
			
			if(timeSpan.TotalDays>7)
			{
				return string.Format(MnLocalize.GetValue("{0} Weeks"),Mathf.RoundToInt((float)timeSpan.TotalDays/7).ToString(format));
			}
			
			if(timeSpan.TotalDays>1)
			{
				return string.Format(MnLocalize.GetValue("{0} Days"),Mathf.RoundToInt((float)timeSpan.TotalDays).ToString(format));
			}
			
			if(timeSpan.TotalHours>1)
			{
				return string.Format(MnLocalize.GetValue("{0} Hours"),Mathf.RoundToInt((float)timeSpan.TotalHours).ToString(format));
			}
			
			if(timeSpan.TotalMinutes>1)
			{
				return string.Format(MnLocalize.GetValue("{0} Minutes"),Mathf.RoundToInt((float)timeSpan.TotalMinutes).ToString(format));
			}
			
			if(timeSpan.TotalSeconds>1)
			{
				return string.Format(MnLocalize.GetValue("{0} Seconds"),Mathf.RoundToInt((float)timeSpan.TotalSeconds).ToString(format));
			}
			
			return "-";
		}
	}
}
#endif

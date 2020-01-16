#if xLibv3
using System;
using UnityEngine;

namespace xLib
{
	public static class ExtTimeSpan
	{
		public static string ToMax(this TimeSpan timeSpan)
		{
			if(timeSpan.TotalDays>365)
			{
				return string.Format(MnLocalize.GetValue("{0} Years"),Mathf.RoundToInt((float)timeSpan.TotalDays/365));
			}
			
			if(timeSpan.TotalDays>30)
			{
				return string.Format(MnLocalize.GetValue("{0} Months"),Mathf.RoundToInt((float)timeSpan.TotalDays/30));
			}
			
			if(timeSpan.TotalDays>7)
			{
				return string.Format(MnLocalize.GetValue("{0} Weeks"),Mathf.RoundToInt((float)timeSpan.TotalDays/7));
			}
			
			if(timeSpan.TotalDays>1)
			{
				return string.Format(MnLocalize.GetValue("{0} Days"),Mathf.RoundToInt((float)timeSpan.TotalDays));
			}
			
			if(timeSpan.TotalHours>1)
			{
				return string.Format(MnLocalize.GetValue("{0} Hours"),Mathf.RoundToInt((float)timeSpan.TotalHours));
			}
			
			if(timeSpan.TotalMinutes>1)
			{
				return string.Format(MnLocalize.GetValue("{0} Minutes"),Mathf.RoundToInt((float)timeSpan.TotalMinutes));
			}
			
			if(timeSpan.TotalSeconds>1)
			{
				return string.Format(MnLocalize.GetValue("{0} Seconds"),Mathf.RoundToInt((float)timeSpan.TotalSeconds));
			}
			
			return "-";
		}
	}
}
#endif

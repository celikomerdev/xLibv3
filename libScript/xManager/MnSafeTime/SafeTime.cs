#if xLibv3
using System;
using UnityEngine;
using xLib.EventClass;

namespace xLib
{
	public static class SafeTime
	{
		public static readonly EventUnity onCalibrate = new EventUnity();
		
		private static DateTime dateTimeAnchor = DateTime.MinValue;
		public static DateTime NowUtc
		{
			get
			{
				return dateTimeAnchor.AddSeconds(Time.unscaledTime);
			}
			set
			{
				if(xLogger.CanDebug) Debug.Log($"SafeTime:{value.ToString()}:NowUtc");
				dateTimeAnchor = value.AddSeconds(-Time.unscaledTime);
				onCalibrate.Invoke();
			}
		}
		
		public static DateTime NowLocal
		{
			get
			{
				return NowUtc.ToLocalTime();
			}
			set
			{
				if(xLogger.CanDebug) Debug.Log($"SafeTime:{value.ToString()}:NowLocal");
				NowUtc = value.ToUniversalTime();
			}
		}
		
		public static DateTime Today
		{
			get
			{
				return NowUtc.StartOfDay();
			}
		}
	}
}
#endif
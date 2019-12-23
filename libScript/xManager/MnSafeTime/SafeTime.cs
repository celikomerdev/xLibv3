#if xLibv3
using System;
using UnityEngine;
using xLib.EventClass;

namespace xLib
{
	public static class SafeTime
	{
		public static EventUnity onCalibrate = new EventUnity();
		
		private static DateTime dateTimeAnchor = DateTime.MinValue;
		public static DateTime UtcNow
		{
			set
			{
				xDebug.LogTempFormat("SafeTime:UtcNow:{0}",value.ToString());
				dateTimeAnchor = value.AddSeconds(-Time.unscaledTime);
				onCalibrate.Invoke();
			}
			get
			{
				return dateTimeAnchor.AddSeconds(Time.unscaledTime);
			}
		}
		
		public static DateTime Now
		{
			set
			{
				UtcNow = value.ToUniversalTime();
			}
			get
			{
				return UtcNow.ToLocalTime();
			}
		}
	}
}
#endif
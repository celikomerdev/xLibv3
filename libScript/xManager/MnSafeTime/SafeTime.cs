﻿#if xLibv3
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
			get
			{
				return dateTimeAnchor.AddSeconds(Time.unscaledTime);
			}
			set
			{
				xDebug.LogTempFormat("SafeTime:UtcNow:{0}",value.ToString());
				dateTimeAnchor = value.AddSeconds(-Time.unscaledTime);
				onCalibrate.Invoke();
			}
		}
		
		public static DateTime Now
		{
			get
			{
				return UtcNow.ToLocalTime();
			}
			set
			{
				UtcNow = value.ToUniversalTime();
			}
		}
		
		public static DateTime Today
		{
			get
			{
				return UtcNow.StartOfDay();
			}
		}
	}
}
#endif
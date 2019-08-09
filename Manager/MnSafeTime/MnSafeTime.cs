#if xLibv3
using System;
using UnityEngine;
using xLib.EventClass;

namespace xLib
{
	public class MnSafeTime : SingletonM<MnSafeTime>
	{
		[SerializeField]private xDateTime dateTimeDefault;
		[SerializeField]private EventUnity onCalibrate = new EventUnity();
		
		public DateTime UtcNow
		{
			set
			{
				if(CanDebug) Debug.LogFormat(this,this.name+"UtcNow:{0}",value.ToString());
				SafeTime.UtcNow = value;
				onCalibrate.Invoke();
			}
		}
		
		protected override void Awaked()
		{
			dateTimeDefault.Init();
			UtcNow = dateTimeDefault.dateTime;
		}
	}
	
	public static class SafeTime
	{
		public static EventUnity onCalibrate = new EventUnity();
		
		private static DateTime dateTimeAnchor;
		public static DateTime UtcNow
		{
			set
			{
				if(xDebug.CanDebug) Debug.LogFormat("SafeTime:UtcNow:{0}",value.ToString());
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
#if xLibv3
using System;
using UnityEngine;
using UnityEngine.Events;
using xLib.EventClass;

namespace xLib
{
	public class MnSafeTime : SingletonM<MnSafeTime>
	{
		[SerializeField]private xDateTime dateTimeDefault;
		[SerializeField]private EventUnity onCalibrate = new EventUnity();
		
		protected override void Awaked()
		{
			SafeTime.onCalibrate.AddListener(OnCalibrate);
			dateTimeDefault.Init();
			if(dateTimeDefault.dateTime>UtcNow) UtcNow = dateTimeDefault.dateTime;
		}
		
		protected override void OnDestroyed()
		{
			SafeTime.onCalibrate.RemoveListener(OnCalibrate);
		}
		
		public DateTime UtcNow
		{
			set
			{
				SafeTime.UtcNow = value;
			}
			get
			{
				return SafeTime.UtcNow;
			}
		}
		
		private void OnCalibrate()
		{
			onCalibrate.Invoke();
		}
	}
	
	public static class SafeTime
	{
		public static UnityEvent onCalibrate = new UnityEvent();
		
		private static DateTime dateTimeAnchor = DateTime.MinValue;
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
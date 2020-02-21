#if xLibv3
using System;
using UnityEngine;
using xLib.EventClass;

namespace xLib
{
	public class MnSafeTime : SingletonM<MnSafeTime>
	{
		[SerializeField]private xDateTime dateTimeDefault = new xDateTime();
		[SerializeField]private EventUnity onCalibrate = new EventUnity();
		
		protected override void Awaked()
		{
			SafeTime.onCalibrate.eventUnity.AddListener(OnCalibrate);
			dateTimeDefault.Init();
			if(dateTimeDefault.dateTime>UtcNow) UtcNow = dateTimeDefault.dateTime;
		}
		
		protected override void OnDestroyed()
		{
			SafeTime.onCalibrate.eventUnity.RemoveListener(OnCalibrate);
		}
		
		public static DateTime UtcNow
		{
			get
			{
				return SafeTime.NowUtc;
			}
			set
			{
				SafeTime.NowUtc = value;
			}
		}
		
		private void OnCalibrate()
		{
			onCalibrate.Invoke();
		}
	}
}
#endif
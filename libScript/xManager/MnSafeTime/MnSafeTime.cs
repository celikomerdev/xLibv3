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
}
#endif
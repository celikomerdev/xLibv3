#if xLibv3
using System;
using UnityEngine;
using xLib.EventClass;

namespace xLib
{
	public class MnSafeTime : SingletonM<MnSafeTime>
	{
		[SerializeField]private xDateTime dateTimeDefault;
		[SerializeField]private EventUnity onRefresh = new EventUnity();
		
		private DateTime dateTimeAnchor;
		public DateTime DateTimeUtc
		{
			set
			{
				if(CanDebug) Debug.LogFormat(this,this.name+"DateTimeUtc:{0}",value.ToString());
				dateTimeAnchor = value.AddSeconds(-Time.unscaledTime);
				onRefresh.Invoke();
			}
			get
			{
				return dateTimeAnchor.AddSeconds(Time.unscaledTime);
			}
		}
		
		protected override void Awaked()
		{
			dateTimeDefault.Init();
			DateTimeUtc = dateTimeDefault.dateTime;
		}
	}
}
#endif
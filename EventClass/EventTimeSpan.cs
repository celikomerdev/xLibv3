#if xLibv3
using System;
using UnityEngine;
using xLib.EventBase;

namespace xLib.EventClass
{
	[Serializable]
	public class EventTimeSpan
	{
		[SerializeField]private EventBaseTimeSpan eventLong = new EventBaseTimeSpan();
		
		public void Invoke(TimeSpan arg0)
		{
			eventLong.Invoke(arg0);
		}
	}
}
#endif
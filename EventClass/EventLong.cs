#if xLibv3
using System;
using UnityEngine;
using xLib.EventBase;

namespace xLib.EventClass
{
	[Serializable]
	public class EventLong
	{
		[SerializeField]public EventBaseLong eventLong = new EventBaseLong();
		
		public void Invoke(long arg0)
		{
			eventLong.Invoke(arg0);
		}
	}
}
#endif
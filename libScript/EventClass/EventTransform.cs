#if xLibv3
using System;
using UnityEngine;
using xLib.EventBase;

namespace xLib.EventClass
{
	[Serializable]
	public class EventTransform
	{
		public EventBaseTransform eventTransform = new EventBaseTransform();
		
		public void Invoke(Transform arg0)
		{
			eventTransform.Invoke(arg0);
		}
		
		public Transform Value
		{
			set
			{
				Invoke(value);
			}
		}
	}
}
#endif
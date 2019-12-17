#if xLibv3
using System;
using UnityEngine;
using xLib.EventBase;

namespace xLib.EventClass
{
	[Serializable]
	public class EventObject
	{
		[SerializeField]private EventBaseObject eventObject = new EventBaseObject();
		
		public void Invoke(object arg0)
		{
			eventObject.Invoke(arg0);
		}
	}
}
#endif
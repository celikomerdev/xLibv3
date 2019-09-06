#if xLibv3
using System;
using UnityEngine;
using xLib.EventBase;

namespace xLib.EventClass
{
	[Serializable]
	public class EventObject
	{
		[SerializeField]public EventBaseObject eventObject = new EventBaseObject();
		
		public void Invoke(object arg0)
		{
			eventObject.Invoke(arg0);
		}
		
		public object Value
		{
			set
			{
				Invoke(value);
			}
		}
	}
}
#endif
#if xLibv3
using System;
using UnityEngine;
using UnityEngine.Networking;
using xLib.EventBase;

namespace xLib.EventClass
{
	[Serializable]
	public class EventUWR
	{
		[SerializeField]public EventBaseUWR eventUWR = new EventBaseUWR();
		
		public void Invoke(UnityWebRequest arg0)
		{
			eventUWR.Invoke(arg0);
		}
		
		public UnityWebRequest Value
		{
			set
			{
				Invoke(value);
			}
		}
	}
}
#endif
#if xLibv3
using System;
using UnityEngine;
using xLib.EventBase;

namespace xLib.EventClass
{
	[Serializable]
	public class EventString
	{
		[SerializeField]private EventBaseString eventString = new EventBaseString();
		
		public void Invoke(string arg0)
		{
			eventString.Invoke(arg0);
		}
	}
}
#endif
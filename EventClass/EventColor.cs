#if xLibv3
using System;
using UnityEngine;
using xLib.EventBase;

namespace xLib.EventClass
{
	[Serializable]
	public class EventColor
	{
		[SerializeField]private EventBaseColor eventColor = new EventBaseColor();
		
		public void Invoke(Color arg0)
		{
			eventColor.Invoke(arg0);
		}
	}
}
#endif
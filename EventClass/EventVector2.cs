#if xLibv3
using System;
using UnityEngine;
using xLib.EventBase;

namespace xLib.EventClass
{
	[Serializable]
	public class EventVector2
	{
		[SerializeField]private EventBaseVector2 eventVector2 = new EventBaseVector2();
		
		public void Invoke(Vector2 arg0)
		{
			eventVector2.Invoke(arg0);
		}
	}
}
#endif
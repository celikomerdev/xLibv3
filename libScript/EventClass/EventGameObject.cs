#if xLibv3
using System;
using UnityEngine;
using xLib.EventBase;

namespace xLib.EventClass
{
	[Serializable]
	public class EventGameObject
	{
		[SerializeField]public EventBaseGameObject eventGameObject = new EventBaseGameObject();
		
		public void Invoke(GameObject arg0)
		{
			eventGameObject.Invoke(arg0);
		}
	}
}
#endif
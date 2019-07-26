#if xLibv3
using System;
using UnityEngine;
using xLib.EventBase;

namespace xLib.EventClass
{
	[Serializable]
	public class EventUnity
	{
		[SerializeField]internal EventBaseUnity eventUnity = new EventBaseUnity();
		
		public void Invoke()
		{
			eventUnity.Invoke();
		}
	}
}
#endif
#if xLibv3
#if ModWebWWW
using System;
using UnityEngine;
using xLib.EventBase;

namespace xLib.EventClass
{
	[Serializable]
	public class EventWWW
	{
		[SerializeField]public EventBaseWWW eventWWW = new EventBaseWWW();
		
		public void Invoke(WWW arg0)
		{
			eventWWW.Invoke(arg0);
		}
	}
}
#endif
#endif
#if xLibv3
using System;
using UnityEngine;
using xLib.EventBase;

namespace xLib.EventClass
{
	[Serializable]
	public class EventVector4
	{
		[SerializeField]public EventBaseVector4 eventVector4 = new EventBaseVector4();
		
		public void Invoke(Vector4 arg0)
		{
			eventVector4.Invoke(arg0);
		}
	}
}
#endif
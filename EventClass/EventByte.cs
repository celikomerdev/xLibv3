#if xLibv3
using System;
using UnityEngine;
using xLib.EventBase;

namespace xLib.EventClass
{
	[Serializable]
	public class EventByte
	{
		[SerializeField]private EventBaseByte eventByte = new EventBaseByte();
		
		public void Invoke(byte arg0)
		{
			eventByte.Invoke(arg0);
		}
	}
}
#endif
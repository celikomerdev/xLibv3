#if xLibv3
using System;
using UnityEngine;
using xLib.EventBase;

namespace xLib.EventClass
{
	[Serializable]
	public class EventInt
	{
		[SerializeField]public EventBaseInt eventInt = new EventBaseInt();
		
		public void Invoke(int arg0)
		{
			eventInt.Invoke(arg0);
		}
		
		public int Value
		{
			set
			{
				Invoke(value);
			}
		}
	}
}
#endif
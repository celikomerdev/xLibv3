#if xLibv3
using System;
using UnityEngine;
using xLib.EventBase;

namespace xLib.EventClass
{
	[Serializable]
	public class EventFloat
	{
		[SerializeField]public EventBaseFloat eventFloat = new EventBaseFloat();
		
		public void Invoke(float arg0)
		{
			eventFloat.Invoke(arg0);
		}
		
		public float Value
		{
			set
			{
				Invoke(value);
			}
		}
	}
}
#endif
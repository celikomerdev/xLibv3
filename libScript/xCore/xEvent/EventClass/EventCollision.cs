#if xLibv3
using System;
using UnityEngine;
using xLib.EventBase;

namespace xLib.EventClass
{
	[Serializable]
	public class EventCollision
	{
		[SerializeField]public EventBaseCollision eventCollision = new EventBaseCollision();
		
		public void Invoke(Collision arg0)
		{
			eventCollision.Invoke(arg0);
		}
		
		public Collision Value
		{
			set
			{
				Invoke(value);
			}
		}
	}
}
#endif
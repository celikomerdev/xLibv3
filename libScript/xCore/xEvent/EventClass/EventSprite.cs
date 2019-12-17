#if xLibv3
using System;
using UnityEngine;
using xLib.EventBase;

namespace xLib.EventClass
{
	[Serializable]
	public class EventSprite
	{
		[SerializeField]public EventBaseSprite eventSprite = new EventBaseSprite();
		
		public void Invoke(Sprite arg0)
		{
			eventSprite.Invoke(arg0);
		}
		
		public Sprite Value
		{
			set
			{
				Invoke(value);
			}
		}
	}
}
#endif
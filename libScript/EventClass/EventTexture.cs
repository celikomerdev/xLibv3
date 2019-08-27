#if xLibv3
using System;
using UnityEngine;
using xLib.EventBase;

namespace xLib.EventClass
{
	[Serializable]
	public class EventTexture
	{
		[SerializeField]internal EventBaseTexture eventTexture = new EventBaseTexture();
		
		public void Invoke(Texture arg0)
		{
			eventTexture.Invoke(arg0);
		}
	}
}
#endif
#if xLibv3
using System;
using UnityEngine;
using xLib.EventBase;

namespace xLib.EventClass
{
	[Serializable]
	public class EventMaterial
	{
		[SerializeField]private EventBaseMaterial eventMaterial = new EventBaseMaterial();
		
		public void Invoke(Material arg0)
		{
			eventMaterial.Invoke(arg0);
		}
	}
}
#endif
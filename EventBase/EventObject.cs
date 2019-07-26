#if xLibv3
using System;
using UnityEngine.Events;

namespace xLib.EventBase
{
	[Serializable]
	public class EventObject : UnityEvent<object>{}
}
#endif
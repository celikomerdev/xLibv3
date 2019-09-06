#if xLibv3
using System;
using UnityEngine.Events;

namespace xLib.EventBase
{
	[Serializable]
	public class EventBaseTimeSpan : UnityEvent<TimeSpan>{}
}
#endif
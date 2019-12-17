#if xLibv3
#if ModWebWWW
using System;
using UnityEngine;
using UnityEngine.Events;

namespace xLib.EventBase
{
	[Serializable]
	public class EventBaseWWW : UnityEvent<WWW>{}
}
#endif
#endif
#if xLibv3
#if WWW
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
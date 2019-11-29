#if xLibv3
using System;
using UnityEngine.Events;
using UnityEngine.Networking;

namespace xLib.EventBase
{
	[Serializable]
	public class EventBaseUWR : UnityEvent<UnityWebRequest>{}
}
#endif
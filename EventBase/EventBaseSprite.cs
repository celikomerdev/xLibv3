#if xLibv3
using System;
using UnityEngine;
using UnityEngine.Events;

namespace xLib.EventBase
{
	[Serializable]
	public class EventBaseSprite : UnityEvent<Sprite>{}
}
#endif
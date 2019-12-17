#if xLibv3
#if ModAudio
using System;
using UnityEngine;
using UnityEngine.Events;

namespace xLib.EventBase
{
	[Serializable]
	public class EventBaseAudioClip : UnityEvent<AudioClip>{}
}
#endif
#endif
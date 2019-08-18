#if xLibv3
#if ModAudio
using System;
using UnityEngine;
using xLib.EventBase;

namespace xLib.EventClass
{
	[Serializable]
	public class EventAudioClip
	{
		[SerializeField]private EventBaseAudioClip eventAudioClip = new EventBaseAudioClip();
		
		public void Invoke(AudioClip arg0)
		{
			eventAudioClip.Invoke(arg0);
		}
	}
}
#endif
#endif
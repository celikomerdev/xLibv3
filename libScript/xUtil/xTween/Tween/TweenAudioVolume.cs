#if xLibv3
#if ModAudio
using UnityEngine;

namespace xLib.xTween
{
	public class TweenAudioSourceVolume : Tween
	{
		[SerializeField]private AudioSource target = null;
		[SerializeField]private float from = 0;
		[SerializeField]private float to = 0;
		
		protected override void SetRatio(float value)
		{
			if(!target) return;
			target.volume = Mathf.LerpUnclamped(from,to,value);
		}
	}
}
#endif
#endif
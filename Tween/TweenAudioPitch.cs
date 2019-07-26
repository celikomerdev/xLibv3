#if xLibv3
using UnityEngine;

namespace xLib.xTween
{
	public class TweenAudioSourcePitch : Tween
	{
		[SerializeField]private AudioSource target = null;
		[SerializeField]private float from = 0;
		[SerializeField]private float to = 0;
		
		override protected void SetRatio(float value)
		{
			if(!target) return;
			target.pitch = Mathf.LerpUnclamped(from,to,value);
		}
	}
}
#endif
#if xLibv3
using UnityEngine;
using UnityEngine.Audio;

namespace xLib.xTween
{
	public class TweenAudioMixerFloat : Tween
	{
		[SerializeField]private AudioMixer target = null;
		[SerializeField]private string key = "";
		[SerializeField]private float from = 0;
		[SerializeField]private float to = 0;
		
		override protected void SetRatio(float value)
		{
			target.SetFloat(key, Mathf.LerpUnclamped(from,to,value));
		}
		
		public float To
		{
			get
			{
				return this.to;
			}
			set
			{
				target.GetFloat(key,out from);
				this.to = value;
			}
		}
	}
}
#endif
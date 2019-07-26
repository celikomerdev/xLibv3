#if xLibv2
using UnityEngine;
using UnityEngine.UI;

namespace xLib.ToolTween
{
	public class TweenSliderValue : Tween
	{
		public Slider target;
		public float from;
		public float to;
		
		override protected void SetRatio(float value)
		{
			target.value = Mathf.LerpUnclamped(from,to,value);
		}
	}
}
#endif
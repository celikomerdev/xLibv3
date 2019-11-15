#if xLibv3
#if PackUI
using UnityEngine;
using UnityEngine.UI;

namespace xLib.xTween
{
	public class TweenSliderValue : Tween
	{
		[SerializeField]private Slider target = null;
		[SerializeField]private float from = 0;
		[SerializeField]private float to = 1;
		
		protected override void SetRatio(float value)
		{
			target.value = Mathf.LerpUnclamped(from,to,value);
		}
	}
}
#endif
#endif
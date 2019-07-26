#if xLibv3
using UnityEngine;
using UnityEngine.UI;

namespace xLib.ToolTween
{
	public class TweenSliderValue : Tween
	{
		[SerializeField]private Slider target;
		[SerializeField]private float from;
		[SerializeField]private float to;
		
		override protected void SetRatio(float value)
		{
			target.value = Mathf.LerpUnclamped(from,to,value);
		}
	}
}
#endif
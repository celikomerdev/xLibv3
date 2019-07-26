#if xLibv3
using UnityEngine;

namespace xLib.xTween
{
	public class TweenCanvasAlpha : Tween
	{
		[SerializeField]private CanvasGroup target;
		[SerializeField]private float from;
		[SerializeField]private float to;
		
		override protected void SetRatio(float value)
		{
			target.alpha = Mathf.LerpUnclamped(from,to,value);
		}
	}
}
#endif
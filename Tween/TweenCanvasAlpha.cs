#if xLibv3
using UnityEngine;

namespace xLib.xTween
{
	public class TweenCanvasAlpha : Tween
	{
		[SerializeField]private CanvasGroup target = null;
		[SerializeField]private float from = 0;
		[SerializeField]private float to = 1;
		
		override protected void SetRatio(float value)
		{
			target.alpha = Mathf.LerpUnclamped(from,to,value);
		}
	}
}
#endif
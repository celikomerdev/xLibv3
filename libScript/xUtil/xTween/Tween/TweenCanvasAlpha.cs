#if xLibv3
#if ModUI
using UnityEngine;

namespace xLib.xTween
{
	public class TweenCanvasAlpha : Tween
	{
		[SerializeField]private CanvasGroup target = null;
		[SerializeField]private float from = 0;
		[SerializeField]private float to = 1;
		
		protected override void SetRatio(float value)
		{
			target.alpha = Mathf.LerpUnclamped(from,to,value);
		}
	}
}
#endif
#endif
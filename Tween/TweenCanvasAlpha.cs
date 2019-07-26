#if xLibv2
using UnityEngine;

namespace xLib.ToolTween
{
	public class TweenCanvasAlpha : Tween
	{
		public CanvasGroup target;
		public float from;
		public float to;
		
		override protected void SetRatio(float value)
		{
			target.alpha = Mathf.LerpUnclamped(from,to,value);
		}
	}
}
#endif
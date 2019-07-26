#if xLibv2
using UnityEngine;

namespace xLib.ToolTween
{
	public class TweenRectOffset : Tween
	{
		public RectTransform target;
		
		[Tooltip("Left - Bottom")]
		public Vector2 fromMin;
		[Tooltip("Right - Top")]
		public Vector2 fromMax;
		
		[Tooltip("Left - Bottom")]
		public Vector2 toMin;
		[Tooltip("Right - Top")]
		public Vector2 toMax;
		
		override protected void SetRatio(float value)
		{
			target.offsetMin = Vector2.LerpUnclamped(fromMin,toMin,value);
			target.offsetMax = Vector2.LerpUnclamped(fromMax,toMax,value);
		}
	}
}
#endif
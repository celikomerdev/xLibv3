#if xLibv2
using UnityEngine;

namespace xLib.ToolTween
{
	public class TweenRectAnchor : Tween
	{
		public RectTransform target;
		public Vector2 fromMin;
		public Vector2 fromMax;
		public Vector2 toMin;
		public Vector2 toMax;
		
		override protected void SetRatio(float value)
		{
			target.anchorMin = Vector3.LerpUnclamped(fromMin, toMin, value);
			target.anchorMax = Vector3.LerpUnclamped(fromMax, toMax, value);
		}
	}
}
#endif
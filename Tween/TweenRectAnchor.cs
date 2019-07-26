#if xLibv3
using UnityEngine;

namespace xLib.xTween
{
	public class TweenRectAnchor : Tween
	{
		[SerializeField]private RectTransform target;
		[SerializeField]private Vector2 fromMin;
		[SerializeField]private Vector2 fromMax;
		[SerializeField]private Vector2 toMin;
		[SerializeField]private Vector2 toMax;
		
		override protected void SetRatio(float value)
		{
			target.anchorMin = Vector3.LerpUnclamped(fromMin, toMin, value);
			target.anchorMax = Vector3.LerpUnclamped(fromMax, toMax, value);
		}
	}
}
#endif
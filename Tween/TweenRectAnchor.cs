#if xLibv3
using UnityEngine;

namespace xLib.xTween
{
	public class TweenRectAnchor : Tween
	{
		[SerializeField]private RectTransform target = null;
		[SerializeField]private Vector2 fromMin = Vector2.zero;
		[SerializeField]private Vector2 fromMax = Vector2.one;
		[SerializeField]private Vector2 toMin = Vector2.zero;
		[SerializeField]private Vector2 toMax = Vector2.one;
		
		override protected void SetRatio(float value)
		{
			target.anchorMin = Vector3.LerpUnclamped(fromMin, toMin, value);
			target.anchorMax = Vector3.LerpUnclamped(fromMax, toMax, value);
		}
	}
}
#endif
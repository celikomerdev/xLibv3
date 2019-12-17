#if xLibv3
using UnityEngine;

namespace xLib.xTween
{
	public class TweenRectOffset : Tween
	{
		[SerializeField]private RectTransform target = null;
		
		[Tooltip("Left - Bottom")]
		[SerializeField]private Vector2 fromMin = Vector2.zero;
		[Tooltip("Right - Top")]
		[SerializeField]private Vector2 fromMax = Vector2.one;
		
		[Tooltip("Left - Bottom")]
		[SerializeField]private Vector2 toMin = Vector2.zero;
		[Tooltip("Right - Top")]
		[SerializeField]private Vector2 toMax = Vector2.one;
		
		protected override void SetRatio(float value)
		{
			target.offsetMin = Vector2.LerpUnclamped(fromMin,toMin,value);
			target.offsetMax = Vector2.LerpUnclamped(fromMax,toMax,value);
		}
	}
}
#endif
#if xLibv3
using UnityEngine;

namespace xLib.ToolTween
{
	public class TweenRectOffset : Tween
	{
		[SerializeField]private RectTransform target;
		
		[Tooltip("Left - Bottom")]
		[SerializeField]private Vector2 fromMin;
		[Tooltip("Right - Top")]
		[SerializeField]private Vector2 fromMax;
		
		[Tooltip("Left - Bottom")]
		[SerializeField]private Vector2 toMin;
		[Tooltip("Right - Top")]
		[SerializeField]private Vector2 toMax;
		
		override protected void SetRatio(float value)
		{
			target.offsetMin = Vector2.LerpUnclamped(fromMin,toMin,value);
			target.offsetMax = Vector2.LerpUnclamped(fromMax,toMax,value);
		}
	}
}
#endif
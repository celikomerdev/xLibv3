#if xLibv3
using UnityEngine;

namespace xLib.xTween
{
	public class TweenAnchoredPosition2D : Tween
	{
		[SerializeField]private RectTransform target = null;
		[SerializeField]private Vector2 from = Vector2.zero;
		[SerializeField]private Vector2 to = Vector2.zero;
		
		override protected void SetRatio(float value)
		{
			target.anchoredPosition = Vector2.LerpUnclamped(from,to,value);
		}
	}
}
#endif
#if xLibv3
using UnityEngine;

namespace xLib.xTween
{
	public class TweenRectSize : Tween
	{
		[SerializeField]private RectTransform target = null;
		[SerializeField]private RectTransform.Axis axis = RectTransform.Axis.Vertical;
		[SerializeField]private float from = 0;
		[SerializeField]private float to = 1;
		
		override protected void SetRatio(float value)
		{
			target.SetSizeWithCurrentAnchors(axis,Mathf.LerpUnclamped(from,to,value));
		}
	}
}
#endif
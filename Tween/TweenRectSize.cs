#if xLibv2
using UnityEngine;

namespace xLib.ToolTween
{
	public class TweenRectSize : Tween
	{
		public RectTransform target;
		public RectTransform.Axis axis = RectTransform.Axis.Vertical;
		public float from = 0;
		public float to = 1;
		
		override protected void SetRatio(float value)
		{
			target.SetSizeWithCurrentAnchors(axis,Mathf.LerpUnclamped(from,to,value));
		}
	}
}
#endif
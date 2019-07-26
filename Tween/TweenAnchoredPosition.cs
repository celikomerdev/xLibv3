#if xLibv3
using UnityEngine;

namespace xLib.ToolTween
{
	public class TweenAnchoredPosition3D : Tween
	{
		[SerializeField]private RectTransform target = null;
		[SerializeField]private Vector3 from = Vector3.zero;
		[SerializeField]private Vector3 to = Vector3.zero;
		
		override protected void SetRatio(float value)
		{
			target.anchoredPosition3D = Vector3.LerpUnclamped(from,to,value);
		}
	}
}
#endif
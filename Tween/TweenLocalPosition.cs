#if xLibv2
using UnityEngine;

namespace xLib.ToolTween
{
	public class TweenLocalPosition : Tween
	{
		public Transform target;
		public Vector3 from;
		public Vector3 to;
		
		override protected void SetRatio(float value)
		{
			target.localPosition = Vector3.LerpUnclamped(from,to,value);
		}
	}
}
#endif
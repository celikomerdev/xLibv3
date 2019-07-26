#if xLibv2
using UnityEngine;

namespace xLib.ToolTween
{
	public class TweenLocalEulerSlerp : Tween
	{
		public Transform target;
		public Vector3 from;
		public Vector3 to;
		
		override protected void SetRatio(float value)
		{
			target.localEulerAngles = Vector3.SlerpUnclamped(from,to,value);
		}
	}
}
#endif
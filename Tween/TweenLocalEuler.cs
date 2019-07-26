#if xLibv2
using UnityEngine;

namespace xLib.ToolTween
{
	public class TweenLocalEuler : Tween
	{
		public Transform target;
		public Vector3 from;
		public Vector3 to;
		
		override protected void SetRatio(float value)
		{
			target.localEulerAngles = Vector3.LerpUnclamped(from,to,value);
		}
	}
}
#endif
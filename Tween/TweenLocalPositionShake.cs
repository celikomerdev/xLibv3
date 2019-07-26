#if xLibv2
using UnityEngine;

namespace xLib.ToolTween
{
	public class TweenLocalPositionShake : Tween
	{
		public Transform target;
		public Vector3 from;
		public Vector3 to;
		
		override protected void SetRatio(float value)
		{
			Vector3 vectorTarget = Vector3.LerpUnclamped(from,to,value);
			target.localPosition = Vector3.Scale(vectorTarget,Random.insideUnitSphere);
		}
	}
}
#endif
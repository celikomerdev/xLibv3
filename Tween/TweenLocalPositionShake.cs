#if xLibv3
using UnityEngine;

namespace xLib.ToolTween
{
	public class TweenLocalPositionShake : Tween
	{
		[SerializeField]private Transform target;
		[SerializeField]private Vector3 from;
		[SerializeField]private Vector3 to;
		
		override protected void SetRatio(float value)
		{
			Vector3 vectorTarget = Vector3.LerpUnclamped(from,to,value);
			target.localPosition = Vector3.Scale(vectorTarget,Random.insideUnitSphere);
		}
	}
}
#endif
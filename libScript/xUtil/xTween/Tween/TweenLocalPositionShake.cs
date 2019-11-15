#if xLibv3
using UnityEngine;

namespace xLib.xTween
{
	public class TweenLocalPositionShake : Tween
	{
		[SerializeField]private Transform target = null;
		[SerializeField]private Vector3 from = Vector3.zero;
		[SerializeField]private Vector3 to = Vector3.zero;
		
		protected override void SetRatio(float value)
		{
			Vector3 vectorTarget = Vector3.LerpUnclamped(from,to,value);
			target.localPosition = Vector3.Scale(vectorTarget,Random.insideUnitSphere);
		}
	}
}
#endif
#if xLibv3
using UnityEngine;

namespace xLib.xTween
{
	public class TweenLocalEulerSlerp : Tween
	{
		[SerializeField]private Transform target = null;
		[SerializeField]private Vector3 from = Vector3.zero;
		[SerializeField]private Vector3 to = Vector3.one;
		
		protected override void SetRatio(float value)
		{
			target.localEulerAngles = Vector3.SlerpUnclamped(from,to,value);
		}
	}
}
#endif
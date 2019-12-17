#if xLibv3
using UnityEngine;

namespace xLib.xTween
{
	public class TweenLocalEulerAdd : Tween
	{
		[SerializeField]private Transform target = null;
		[SerializeField]private Vector3 min = Vector3.zero;
		[SerializeField]private Vector3 max = Vector3.zero;
		
		protected override void SetRatio(float value)
		{
			target.localEulerAngles += Vector3.LerpUnclamped(min,max,value);
		}
	}
}
#endif
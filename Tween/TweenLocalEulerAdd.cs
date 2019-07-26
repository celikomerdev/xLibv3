#if xLibv3
using UnityEngine;

namespace xLib.xTween
{
	public class TweenLocalEulerAdd : Tween
	{
		[SerializeField]private Transform target;
		[SerializeField]private Vector3 min;
		[SerializeField]private Vector3 max;
		
		override protected void SetRatio(float value)
		{
			target.localEulerAngles += Vector3.LerpUnclamped(min,max,value);
		}
	}
}
#endif
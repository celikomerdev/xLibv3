#if xLibv3
using UnityEngine;

namespace xLib.xTween
{
	public class TweenLocalEuler : Tween
	{
		[SerializeField]private Transform target;
		[SerializeField]private Vector3 from;
		[SerializeField]private Vector3 to;
		
		override protected void SetRatio(float value)
		{
			target.localEulerAngles = Vector3.LerpUnclamped(from,to,value);
		}
	}
}
#endif
#if xLibv3
using UnityEngine;

namespace xLib.xTween
{
	public class TweenLocalEuler : Tween
	{
		[SerializeField]private Transform target = null;
		[SerializeField]private Vector3 from = Vector3.zero;
		[SerializeField]private Vector3 to = Vector3.zero;
		
		override protected void SetRatio(float value)
		{
			target.localEulerAngles = Vector3.LerpUnclamped(from,to,value);
		}
	}
}
#endif
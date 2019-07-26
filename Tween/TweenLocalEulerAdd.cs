#if xLibv2
using UnityEngine;

namespace xLib.ToolTween
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
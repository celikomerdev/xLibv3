#if xLibv3
using UnityEngine;

namespace xLib.xTween
{
	public class TweenCameraFov : Tween
	{
		[SerializeField]private Camera target;
		[SerializeField]private float from;
		[SerializeField]private float to;
		
		override protected void SetRatio(float value)
		{
			target.fieldOfView = Mathf.LerpUnclamped(from,to,value);
		}
	}
}
#endif
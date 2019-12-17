#if xLibv3
using UnityEngine;

namespace xLib.xTween
{
	public class TweenCameraFov : Tween
	{
		[SerializeField]private Camera target = null;
		[SerializeField]private float from = 0;
		[SerializeField]private float to = 1;
		
		protected override void SetRatio(float value)
		{
			target.fieldOfView = Mathf.LerpUnclamped(from,to,value);
		}
	}
}
#endif
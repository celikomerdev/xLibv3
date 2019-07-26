#if xLibv2
using UnityEngine;

namespace xLib.ToolTween
{
	public class TweenCameraFov : Tween
	{
		public Camera target;
		public float from;
		public float to;
		
		override protected void SetRatio(float value)
		{
			target.fieldOfView = Mathf.LerpUnclamped(from,to,value);
		}
	}
}
#endif
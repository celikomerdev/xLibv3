#if xLibv2
using UnityEngine;
using xLib.ToolSkidmark;

namespace xLib.ToolTween
{
	public class TweenSkidIntensity : Tween
	{
		public PointSkidmark target;
		public float from;
		public float to;
		
		override protected void SetRatio(float value)
		{
			target.Intensity = Mathf.LerpUnclamped(from,to,value);
		}
	}
}
#endif
#if xLibv2
using UnityEngine;
using UnityEngine.Video;

namespace xLib.ToolTween
{
	public class TweenVideoPlayerAlpha : Tween
	{
		public VideoPlayer target;
		public float from;
		public float to;
		
		override protected void SetRatio(float value)
		{
			target.targetCameraAlpha = Mathf.LerpUnclamped(from,to,value);
		}
	}
}
#endif
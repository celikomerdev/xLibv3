#if xLibv3
#if UnityVideo
using UnityEngine;
using UnityEngine.Video;

namespace xLib.ToolTween
{
	public class TweenVideoPlayerAlpha : Tween
	{
		[SerializeField]private VideoPlayer target = null;
		[SerializeField]private float from = 0;
		[SerializeField]private float to = 1;
		
		override protected void SetRatio(float value)
		{
			target.targetCameraAlpha = Mathf.LerpUnclamped(from,to,value);
		}
	}
}
#endif
#endif
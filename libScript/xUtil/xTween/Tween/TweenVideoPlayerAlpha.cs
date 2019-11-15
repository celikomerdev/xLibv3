#if xLibv3
#if ModVideo
using UnityEngine;
using UnityEngine.Video;

namespace xLib.xTween
{
	public class TweenVideoPlayerAlpha : Tween
	{
		[SerializeField]private VideoPlayer target = null;
		[SerializeField]private float from = 0;
		[SerializeField]private float to = 1;
		
		protected override void SetRatio(float value)
		{
			target.targetCameraAlpha = Mathf.LerpUnclamped(from,to,value);
		}
	}
}
#endif
#endif
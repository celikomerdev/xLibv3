﻿#if xLibv3
using UnityEngine;
using UnityEngine.Video;

namespace xLib.xTween
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
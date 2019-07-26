﻿#if xLibv3
using UnityEngine;
using UnityEngine.UI;

namespace xLib.ToolTween
{
	public class TweenGraphicRaycast : Tween
	{
		[SerializeField]private Graphic target;
		[SerializeField]private float threshold = 0;
		
		override protected void SetRatio(float value)
		{
			target.raycastTarget = (value>threshold);
		}
	}
}
#endif
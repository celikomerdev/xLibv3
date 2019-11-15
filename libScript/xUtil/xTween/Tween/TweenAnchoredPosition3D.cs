﻿#if xLibv3
using UnityEngine;

namespace xLib.xTween
{
	public class TweenAnchoredPosition3D : Tween
	{
		[SerializeField]private RectTransform target = null;
		[SerializeField]private Vector3 from = Vector3.zero;
		[SerializeField]private Vector3 to = Vector3.zero;
		
		protected override void SetRatio(float value)
		{
			target.anchoredPosition3D = Vector3.LerpUnclamped(from,to,value);
		}
	}
}
#endif
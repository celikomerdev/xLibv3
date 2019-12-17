﻿#if xLibv3
using UnityEngine;

namespace xLib.xTween
{
	public class TweenGameObject : Tween
	{
		[SerializeField]private GameObject target = null;
		[SerializeField]private float threshold = 0;
		[SerializeField]private bool reverse = false;
		
		override protected void SetRatio(float value)
		{
			bool result = (value>threshold);
			if(reverse) result = !result;
			
			if(!target) return;
			target.SetActive(result);
		}
	}
}
#endif
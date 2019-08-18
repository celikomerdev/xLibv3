#if xLibv3
#if PackUI
using UnityEngine;
using UnityEngine.UI;

namespace xLib.xTween
{
	public class TweenGraphicRaycast : Tween
	{
		[SerializeField]private Graphic target = null;
		[SerializeField]private float threshold = 0;
		
		override protected void SetRatio(float value)
		{
			target.raycastTarget = (value>threshold);
		}
	}
}
#endif
#endif
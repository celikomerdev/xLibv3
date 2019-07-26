#if xLibv2
using UnityEngine;
using UnityEngine.UI;

namespace xLib.ToolTween
{
	public class TweenGraphicRaycast : Tween
	{
		public Graphic target;
		public float threshold = 0;
		
		override protected void SetRatio(float value)
		{
			target.raycastTarget = (value>threshold);
		}
	}
}
#endif
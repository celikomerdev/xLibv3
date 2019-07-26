#if xLibv2
using UnityEngine;
using UnityEngine.UI;

namespace xLib.ToolTween
{
	public class TweenGraphicAlpha : Tween
	{
		public Graphic target;
		public float from;
		public float to;
		
		override protected void SetRatio(float value)
		{
			Color temp = target.color;
			temp.a = Mathf.LerpUnclamped(from,to,value);
			target.color = temp;
		}
	}
}
#endif
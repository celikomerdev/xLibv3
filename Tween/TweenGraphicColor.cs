#if xLibv2
using UnityEngine;
using UnityEngine.UI;

namespace xLib.ToolTween
{
	public class TweenGraphicColor : Tween
	{
		public Graphic target;
		public Color from;
		public Color to;
		
		override protected void SetRatio(float value)
		{
			target.color = Color.LerpUnclamped(from,to,value);
		}
	}
}
#endif
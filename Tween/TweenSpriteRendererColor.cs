#if xLibv2
using UnityEngine;
using UnityEngine.UI;

namespace xLib.ToolTween
{
	public class TweenSpriteRendererColor : Tween
	{
		public SpriteRenderer target;
		public Color from;
		public Color to;
		
		override protected void SetRatio(float value)
		{
			target.color = Color.LerpUnclamped(from,to,value);
		}
	}
}
#endif
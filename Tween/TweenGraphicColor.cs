#if xLibv3
using UnityEngine;
using UnityEngine.UI;

namespace xLib.ToolTween
{
	public class TweenGraphicColor : Tween
	{
		[SerializeField]private Graphic target;
		[SerializeField]private Color from;
		[SerializeField]private Color to;
		
		override protected void SetRatio(float value)
		{
			target.color = Color.LerpUnclamped(from,to,value);
		}
	}
}
#endif
#if xLibv3
using UnityEngine;
using UnityEngine.UI;

namespace xLib.xTween
{
	public class TweenGraphicColor : Tween
	{
		[SerializeField]private Graphic target = null;
		[SerializeField]private Color from = Color.white;
		[SerializeField]private Color to = Color.white;
		
		override protected void SetRatio(float value)
		{
			target.color = Color.LerpUnclamped(from,to,value);
		}
	}
}
#endif
#if xLibv3
#if PackUI
using UnityEngine;
using UnityEngine.UI;

namespace xLib.xTween
{
	public class TweenGraphicAlpha : Tween
	{
		[SerializeField]private Graphic target = null;
		[SerializeField]private float from = 0;
		[SerializeField]private float to = 1;
		
		override protected void SetRatio(float value)
		{
			Color temp = target.color;
			temp.a = Mathf.LerpUnclamped(from,to,value);
			target.color = temp;
		}
	}
}
#endif
#endif
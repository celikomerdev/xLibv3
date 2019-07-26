#if xLibv3
using UnityEngine;
using UnityEngine.UI;

namespace xLib.xTween
{
	public class TweenGraphicAlpha : Tween
	{
		[SerializeField]private Graphic target;
		[SerializeField]private float from;
		[SerializeField]private float to;
		
		override protected void SetRatio(float value)
		{
			Color temp = target.color;
			temp.a = Mathf.LerpUnclamped(from,to,value);
			target.color = temp;
		}
	}
}
#endif
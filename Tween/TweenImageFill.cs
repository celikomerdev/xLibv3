#if xLibv2
using UnityEngine;
using UnityEngine.UI;

namespace xLib.ToolTween
{
	public class TweenImageFill : Tween
	{
		public Image target;
		public float from = 0;
		public float to = 1;
		
		override protected void SetRatio(float value)
		{
			target.fillAmount = Mathf.LerpUnclamped(from,to,value);
		}
	}
}
#endif
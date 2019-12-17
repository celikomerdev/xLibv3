#if xLibv3
#if PackUI
using UnityEngine;
using UnityEngine.UI;

namespace xLib.xTween
{
	public class TweenImageFill : Tween
	{
		public Image target;
		public float from = 0;
		public float to = 1;
		
		protected override void SetRatio(float value)
		{
			target.fillAmount = Mathf.LerpUnclamped(from,to,value);
		}
	}
}
#endif
#endif
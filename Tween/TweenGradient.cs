#if xLibv3
using UnityEngine;
using xLib.EventClass;

namespace xLib.xTween
{
	public class TweenGradient : Tween
	{
		[SerializeField]private Gradient fromTo;
		[SerializeField]private EventColor eventColor;
		
		override protected void SetRatio(float value)
		{
			eventColor.Invoke(fromTo.Evaluate(value));
		}
	}
}
#endif
#if xLibv3
using UnityEngine;
using xLib.EventClass;

namespace xLib.xTween
{
	public class TweenGradient : Tween
	{
		[SerializeField]private Gradient fromTo = new Gradient();
		[SerializeField]private EventColor eventColor = new EventColor();
		
		protected override void SetRatio(float value)
		{
			eventColor.Invoke(fromTo.Evaluate(value));
		}
	}
}
#endif
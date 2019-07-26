#if xLibv2
using UnityEngine;
using xLib.ToolEventClass;

namespace xLib.ToolTween
{
	public class TweenGradient : Tween
	{
		public Gradient fromTo;
		public EventColor eventColor;
		
		override protected void SetRatio(float value)
		{
			eventColor.Invoke(fromTo.Evaluate(value));
		}
	}
}
#endif
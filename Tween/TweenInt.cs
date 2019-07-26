#if xLibv2
using UnityEngine;
using xLib.ToolEventClass;

namespace xLib.ToolTween
{
	public class TweenInt : Tween
	{
		public int from;
		public int to;
		public EventInt eventInt;
		
		override protected void SetRatio(float value)
		{
			eventInt.Invoke(Mathx.MathInt.LerpUnclamped(from,to,value));
		}
		
		public int From
		{
			get
			{
				return from;
			}
			set
			{
				from = value;
			}
		}
		
		public int To
		{
			get
			{
				return to;
			}
			set
			{
				to = value;
			}
		}
	}
}
#endif
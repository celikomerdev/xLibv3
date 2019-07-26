#if xLibv2
using UnityEngine;
using xLib.ToolEventClass;

namespace xLib.ToolTween
{
	public class TweenFloat : Tween
	{
		public float from;
		public float to;
		public EventFloat eventFloat;
		
		override protected void SetRatio(float value)
		{
			eventFloat.Invoke(Mathf.LerpUnclamped(from,to,value));
		}
		
		public float From
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
		
		public float To
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
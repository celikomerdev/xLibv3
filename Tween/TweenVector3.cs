#if xLibv2
using UnityEngine;
using xLib.ToolEventClass;

namespace xLib.ToolTween
{
	public class TweenVector3 : Tween
	{
		public Vector3 from;
		public Vector3 to;
		public EventVector3 eventVector3;
		
		override protected void SetRatio(float value)
		{
			eventVector3.Invoke(Vector3.LerpUnclamped(from,to,value));
		}
		
		public Vector3 From
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
		
		public Vector3 To
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
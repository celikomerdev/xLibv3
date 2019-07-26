#if xLibv2
using UnityEngine;

namespace xLib.ToolTween
{
	public class TweenTransformPosition : Tween
	{
		public Transform target;
		public Transform from;
		public Transform to;
		
		override protected void SetRatio(float value)
		{
			target.position = Vector3.LerpUnclamped(from.position,to.position,value);
		}
		
		public Transform From
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
		
		public Transform To
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
#if xLibv2
using UnityEngine;

namespace xLib.ToolTween
{
	public class TweenTransformRotation : Tween
	{
		public Transform target;
		public Transform from;
		public Transform to;
		
		override protected void SetRatio(float value)
		{
			target.rotation = Quaternion.LerpUnclamped(from.rotation,to.rotation,value);
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
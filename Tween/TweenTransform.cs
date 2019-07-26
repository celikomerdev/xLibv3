#if xLibv2
using UnityEngine;

namespace xLib.ToolTween
{
	public class TweenTransform : Tween
	{
		public Transform target;
		public Transform from;
		public Transform to;
		
		override protected void SetRatio(float value)
		{
			target.Lerp(from,to,value);
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
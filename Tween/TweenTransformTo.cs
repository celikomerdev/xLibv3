#if xLibv2
using UnityEngine;

namespace xLib.ToolTween
{
	public class TweenTransformTo : Tween
	{
		public Transform target;
		public Transform to;
		
		override protected void SetRatio(float value)
		{
			if(to==null) return;
			target.LerpTo(to,value);
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
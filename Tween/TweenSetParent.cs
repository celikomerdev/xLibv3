#if xLibv2
using UnityEngine;

namespace xLib.ToolTween
{
	public class TweenSetParent : Tween
	{
		public Transform target;
		public Transform to;
		public float threshold=1;
		public bool worldPositionStays;
		
		override protected void SetRatio(float value)
		{
			if(value<threshold) return;
			target.SetParent(to);
			if(!worldPositionStays) target.ResetTransform();
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
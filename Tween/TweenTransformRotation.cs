#if xLibv3
using UnityEngine;

namespace xLib.xTween
{
	public class TweenTransformRotation : Tween
	{
		[SerializeField]private Transform target;
		[SerializeField]private Transform from;
		[SerializeField]private Transform to;
		
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
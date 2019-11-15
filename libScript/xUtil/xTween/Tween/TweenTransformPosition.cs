#if xLibv3
using UnityEngine;

namespace xLib.xTween
{
	public class TweenTransformPosition : Tween
	{
		[SerializeField]private Transform target = null;
		[SerializeField]private Transform from = null;
		[SerializeField]private Transform to = null;
		
		protected override void SetRatio(float value)
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
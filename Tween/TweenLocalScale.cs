#if xLibv2
using UnityEngine;

namespace xLib.ToolTween
{
	public class TweenLocalScale : Tween
	{
		public Transform target;
		public Vector3 from = Vector3.one;
		public Vector3 to = Vector3.one;
		
		override protected void SetRatio(float value)
		{
			target.localScale = Vector3.LerpUnclamped(from,to,value);
		}
	}
}
#endif
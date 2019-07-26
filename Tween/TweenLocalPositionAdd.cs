#if xLibv2
using UnityEngine;

namespace xLib.ToolTween
{
	public class TweenLocalPositionAdd : Tween
	{
		public Transform target;
		public Vector3 min;
		public Vector3 max;
		
		override protected void SetRatio(float value)
		{
			#if UNITY_EDITOR
			if(!Application.isPlaying) return;
			#endif
			target.localPosition += Vector3.LerpUnclamped(min,max,value);
		}
	}
}
#endif
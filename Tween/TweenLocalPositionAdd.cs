#if xLibv3
using UnityEngine;

namespace xLib.ToolTween
{
	public class TweenLocalPositionAdd : Tween
	{
		[SerializeField]private Transform target;
		[SerializeField]private Vector3 min;
		[SerializeField]private Vector3 max;
		
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
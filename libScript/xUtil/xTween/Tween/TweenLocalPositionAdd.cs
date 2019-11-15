#if xLibv3
using UnityEngine;

namespace xLib.xTween
{
	public class TweenLocalPositionAdd : Tween
	{
		[SerializeField]private Transform target = null;
		[SerializeField]private Vector3 min = Vector3.zero;
		[SerializeField]private Vector3 max = Vector3.zero;
		
		protected override void SetRatio(float value)
		{
			#if UNITY_EDITOR
			if(!Application.isPlaying) return;
			#endif
			target.localPosition += Vector3.LerpUnclamped(min,max,value);
		}
	}
}
#endif
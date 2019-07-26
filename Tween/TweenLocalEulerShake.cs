#if xLibv2
using UnityEngine;

namespace xLib.ToolTween
{
	public class TweenLocalEulerShake : Tween
	{
		public Transform target;
		public Vector3 from;
		public Vector3 to;
		
		override protected void SetRatio(float value)
		{
			#if UNITY_EDITOR
			if(!Application.isPlaying) return;
			#endif
			
			if(target==null) return;
			Vector3 vectorTarget = Vector3.LerpUnclamped(from,to,value);
			target.localEulerAngles = Vector3.Scale(vectorTarget,Random.insideUnitSphere);
		}
	}
}
#endif
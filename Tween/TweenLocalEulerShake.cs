#if xLibv3
using UnityEngine;

namespace xLib.xTween
{
	public class TweenLocalEulerShake : Tween
	{
		[SerializeField]private Transform target;
		[SerializeField]private Vector3 from;
		[SerializeField]private Vector3 to;
		
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
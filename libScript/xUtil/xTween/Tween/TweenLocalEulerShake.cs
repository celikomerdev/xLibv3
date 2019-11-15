#if xLibv3
using UnityEngine;

namespace xLib.xTween
{
	public class TweenLocalEulerShake : Tween
	{
		[SerializeField]private Transform target = null;
		[SerializeField]private Vector3 from = Vector3.zero;
		[SerializeField]private Vector3 to = Vector3.zero;
		
		protected override void SetRatio(float value)
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
#if xLibv3
using UnityEngine;

namespace xLib.xTween
{
	public class TweenLocalScale : Tween
	{
		[SerializeField]private Transform target;
		[SerializeField]private Vector3 from = Vector3.one;
		[SerializeField]private Vector3 to = Vector3.one;
		
		override protected void SetRatio(float value)
		{
			target.localScale = Vector3.LerpUnclamped(from,to,value);
		}
	}
}
#endif
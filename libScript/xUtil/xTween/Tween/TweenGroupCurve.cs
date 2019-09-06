#if xLibv3
using UnityEngine;

namespace xLib.xTween
{
	public class TweenGroupCurve : TweenGroup
	{
		[SerializeField]private AnimationCurve curve = AnimationCurve.Linear(0,0,1,1);
		
		override protected void SetRatio(float value)
		{
			base.SetRatio(curve.Evaluate(value));
		}
	}
}
#endif
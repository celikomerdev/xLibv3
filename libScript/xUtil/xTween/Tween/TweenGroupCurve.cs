#if xLibv3
using UnityEngine;

namespace xLib.xTween
{
	public class TweenGroupCurve : TweenGroup
	{
		[SerializeField]private AnimationCurve curve = AnimationCurve.Linear(0,0,1,1);
		
		protected override void ApplyRatio(float value)
		{
			base.ApplyRatio(curve.Evaluate(value));
		}
	}
}
#endif
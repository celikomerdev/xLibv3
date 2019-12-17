#if xLibv3
using UnityEngine;

namespace xLib.xTweener
{
	public class TweenerCurve : Tweener
	{
		[Header("Curve")]
		[SerializeField]private AnimationCurve curveForward = AnimationCurve.Linear(0,0,1,1);
		protected override float RatioForward(float value)
		{
			return curveForward.Evaluate(value);
		}
		
		[SerializeField]private AnimationCurve curveBackward = AnimationCurve.Linear(0,0,1,1);
		protected override float RatioBackward(float value)
		{
			return curveBackward.Evaluate(value);
		}
	}
}
#endif
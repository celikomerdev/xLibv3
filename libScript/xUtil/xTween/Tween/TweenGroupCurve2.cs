#if xLibv3
using UnityEngine;

namespace xLib.xTween
{
	public class TweenGroupCurve2 : TweenGroup2
	{
		[Header("Curve")]
		[UnityEngine.Serialization.FormerlySerializedAs("curveForward")]
		[SerializeField]private AnimationCurve curve = AnimationCurve.Linear(0,0,1,1);
		[UnityEngine.Serialization.FormerlySerializedAs("curveBackward")]
		[SerializeField]private AnimationCurve curveBack = AnimationCurve.Linear(0,0,1,1);
		
		protected override float RatioForward(float value)
		{
			return curve.Evaluate(value);
		}
		
		protected override float RatioBackward(float value)
		{
			return curveBack.Evaluate(value);
		}
	}
}
#endif
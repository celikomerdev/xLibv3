#if xLibv3
using UnityEngine;
using xLib.xValueClass;

namespace xLib.xTween
{
	public class TweenGroupNode : TweenGroup
	{
		[Header("Curve")]
		[UnityEngine.Serialization.FormerlySerializedAs("curveForward")]
		[SerializeField]private NodeAnimationCurve curve = null;
		
		protected override void ApplyRatio(float value)
		{
			base.ApplyRatio(curve.Value.Evaluate(value));
		}
	}
}
#endif
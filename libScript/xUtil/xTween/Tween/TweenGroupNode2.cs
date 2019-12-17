#if xLibv3
using UnityEngine;
using xLib.xNode.NodeObject;

namespace xLib.xTween
{
	public class TweenGroupNode2 : TweenGroup2
	{
		[Header("Curve")]
		[UnityEngine.Serialization.FormerlySerializedAs("curveForward")]
		[SerializeField]private NodeAnimationCurve curve = null;
		[UnityEngine.Serialization.FormerlySerializedAs("curveBackward")]
		[SerializeField]private NodeAnimationCurve curveBack = null;
		
		protected override float RatioForward(float value)
		{
			return curve.Value.Evaluate(value);
		}
		
		protected override float RatioBackward(float value)
		{
			return curveBack.Value.Evaluate(value);
		}
	}
}
#endif
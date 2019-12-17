#if xLibv3
using UnityEngine;
using xLib.xNode.NodeObject;
using xLib.xTweener;

namespace xLib.ToolTweener
{
	public class TweenerNode : Tweener
	{
		[Header("Curve")]
		[SerializeField]private NodeAnimationCurve curveForward = null;
		protected override float RatioForward(float value)
		{
			return curveForward.Value.Evaluate(value);
		}
		
		[SerializeField]private NodeAnimationCurve curveBackward = null;
		protected override float RatioBackward(float value)
		{
			return curveBackward.Value.Evaluate(value);
		}
	}
}
#endif
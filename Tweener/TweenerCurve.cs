#if xLibv3
using UnityEngine;

namespace xLib.xTweener
{
	public class TweenerCurve : Tweener
	{
		[Header("Curve")]
		[SerializeField]private AnimationCurve curveForward = AnimationCurve.Linear(0,0,1,1);
		protected override AnimationCurve CurveForward
		{
			get
			{
				return curveForward;
			}
		}
		
		[SerializeField]private AnimationCurve curveBackward = AnimationCurve.Linear(0,0,1,1);
		protected override AnimationCurve CurveBackward
		{
			get
			{
				return curveBackward;
			}
		}
	}
}
#endif
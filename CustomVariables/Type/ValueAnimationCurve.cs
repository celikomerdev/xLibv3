#if xLibv3
using System;
using UnityEngine;

namespace xLib.xValueClass
{
	[Serializable]
	public class ValueAnimationCurve : xValueEqual<AnimationCurve>
	{
		#region Compare
		protected override bool IsEqual(AnimationCurve value)
		{
			if(value.xHashCode() != Value.xHashCode()) return false;
			return (value == Value);
		}
		#endregion
	}
}
#endif
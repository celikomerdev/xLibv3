#if xLibv3
using System;
using UnityEngine;

namespace xLib.xValueClass
{
	[Serializable]
	public class ValueAnimationCurve : xValueEqual<AnimationCurve>
	{
		#region Compare
		protected override bool IsEqual(AnimationCurve valueNew)
		{
			if(valueNew.xHashCode() != Value.xHashCode()) return false;
			return (valueNew.Equals(Value));
		}
		#endregion
	}
}
#endif
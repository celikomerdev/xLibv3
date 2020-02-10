#if xLibv3
using System;

namespace xLib.xValueClass
{
	[Serializable]
	public class ValueFloatArray : xValueEqual<float[]>
	{
		#region Compare
		protected override bool IsEqual(float[] valueNew)
		{
			if(valueNew.xHashCode() != Value.xHashCode()) return false;
			return (valueNew == Value);
		}
		#endregion
	}
}
#endif
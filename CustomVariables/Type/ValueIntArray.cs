#if xLibv3
using System;

namespace xLib.xValueClass
{
	[Serializable]
	public class ValueIntArray : xValueEqual<int[]>
	{
		#region Compare
		protected override bool IsEqual(int[] value)
		{
			if(value.xHashCode() != Value.xHashCode()) return false;
			return (value == Value);
		}
		#endregion
	}
}
#endif
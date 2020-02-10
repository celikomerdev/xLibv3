#if xLibv3
using System;

namespace xLib.xValueClass
{
	[Serializable]
	public class ValueIntArray : xValueEqual<int[]>
	{
		#region Compare
		protected override bool IsEqual(int[] valueNew)
		{
			if(valueNew.xHashCode() != Value.xHashCode()) return false;
			return (valueNew == Value);
		}
		#endregion
	}
}
#endif
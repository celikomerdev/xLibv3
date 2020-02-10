#if xLibv3
using System;

namespace xLib.xValueClass
{
	[Serializable]
	public class ValueBoolArray : xValueEqual<bool[]>
	{
		#region Compare
		protected override bool IsEqual(bool[] valueNew)
		{
			if(valueNew.xHashCode() != Value.xHashCode()) return false;
			return (valueNew == Value);
		}
		#endregion
	}
}
#endif
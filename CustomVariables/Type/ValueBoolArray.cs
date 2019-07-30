﻿#if xLibv3
using System;

namespace xLib.xValueClass
{
	[Serializable]
	public class ValueBoolArray : xValueEqual<bool[]>
	{
		#region Compare
		protected override bool IsEqual(bool[] value)
		{
			if(value.xHashCode() != Value.xHashCode()) return false;
			return (value == Value);
		}
		#endregion
	}
}
#endif
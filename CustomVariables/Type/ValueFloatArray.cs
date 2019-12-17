﻿#if xLibv3
using System;

namespace xLib.xValueClass
{
	[Serializable]
	public class ValueFloatArray : xValueEqual<float[]>
	{
		#region Compare
		protected override bool IsEqual(float[] value)
		{
			if(value.xHashCode() != Value.xHashCode()) return false;
			return (value == Value);
		}
		#endregion
	}
}
#endif
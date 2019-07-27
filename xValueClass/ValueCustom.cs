#if xLibv2Discard
using System;

namespace xLib.xValueClass
{
	[Serializable]
	public class ValueCustom : xValueEqual<ValueCustom.Token>
	{
		#region Token
		[Serializable]
		public struct Token
		{
			public bool valueBool;
			public bool[] arrayBool;
			public int valueInt;
			public float valueFloat;
			public string valueString;
		}
		#endregion
		
		#region Compare
		protected override bool IsEqual(ValueCustom.Token value)
		{
			if(value.xHashCode() != Value.xHashCode()) return false;
			return value.Equals(Value);
		}
		#endregion
	}
}
#endif